using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NetCoreStack.Contracts;
using NetCoreStack.Mvc;
using NetCoreStack.Mvc.Extensions;
using System;
using System.Text;

namespace NetCoreStack.Localization.Test.Hosting.Exceptions
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        private IModelMetadataProvider GetModelMetadataProvider(ExceptionContext context)
        {
            return ServiceProviderServiceExtensions.GetService<IModelMetadataProvider>(context.HttpContext.RequestServices);
        }

        private IHostingEnvironment GetHostingEnvironment(ExceptionContext context)
        {
            return ServiceProviderServiceExtensions.GetService<IHostingEnvironment>(context.HttpContext.RequestServices);
        }

        private IStringLocalizer GetStringLocalizer(ExceptionContext context)
        {
            return ServiceProviderServiceExtensions.GetService<IStringLocalizer>(context.HttpContext.RequestServices);
        }

        private string GetIp(ExceptionContext context)
        {
            return context?.HttpContext.Features?.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.ToString();
        }

        private void CreateErrorViewResult(ExceptionContext context, string contentBody, string logGuid, int statusCode = StatusCodes.Status500InternalServerError, bool isOperationalException = false)
        {
            var result = new ViewResult { ViewName = "Error", StatusCode = statusCode };
            result.ViewData = new ViewDataDictionary(GetModelMetadataProvider(context), context.ModelState);

            var exceptionContextModel = new BasicExceptionContext
            {
                ExceptionType = context.Exception.GetType().FullName,
                ExceptionDetail = contentBody,
                LogGuid = logGuid,
                Message = context.Exception.Message
            };

            var env = GetHostingEnvironment(context);

            if (env.IsProduction())
            {
                if (isOperationalException)
                {
                    exceptionContextModel.IsOperationalException = true;
                    exceptionContextModel.LogGuid = string.Empty;
                }
                else
                    exceptionContextModel.ExceptionDetail = string.Empty;
            }
            else
            {
                if (isOperationalException)
                {
                    exceptionContextModel.IsOperationalException = true;
                    exceptionContextModel.Message = contentBody;
                }
            }

            // Workaround for https://github.com/aspnet/Home/issues/1820
            context.HttpContext.Items.Add(nameof(BasicExceptionContext), exceptionContextModel);
            context.Result = result;
        }

        public override void OnException(ExceptionContext context)
        {
            var stringLocalizer = GetStringLocalizer(context);

            var logGuid = Guid.NewGuid().ToString("N");
            var hostingEnvironment = GetHostingEnvironment(context);
            var contentBody = $"Unexpected error occurred!<br/> Error Code:{logGuid}";
            var isAjaxRequest = context.HttpContext.Request.IsAjaxRequest();
            var isProduction = hostingEnvironment.IsProduction();
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.ExceptionHandled = true; // mark exception as handled

            var exception = context.Exception;

            if (exception.GetType().BaseType() == typeof(OperationalException))
            {
                var operationalException = (OperationalException)exception;
                var operationalExceptionType = operationalException.Type;
                var operationalExceptionTypeDisplayName = operationalExceptionType.GetDisplayName();
                var localizeExceptionType = stringLocalizer[operationalExceptionTypeDisplayName];

                var operationalMessage = new StringBuilder();
                operationalMessage.AppendLine(localizeExceptionType + (String.IsNullOrWhiteSpace(operationalException.CustomParameter) ? "" : $" - {operationalException.CustomParameter}"));

                if (isAjaxRequest)
                {
                    context.Result = new JsonResult(new CustomWebResult(title: "Error", content: operationalMessage.ToString(), resultState: ResultState.Error));
                }
                else
                {
                    CreateErrorViewResult(context, operationalMessage.ToString(), logGuid, StatusCodes.Status400BadRequest, isOperationalException: true);
                }

                // supress internal server error
                context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
                return;
            }

            if (exception == null && context.Exception.InnerException != null)
                exception = context.Exception.InnerException;

            contentBody = $"{ExceptionFormatter.CreateMessage(exception)}{Environment.NewLine}Error Code:{logGuid}";

            if (isAjaxRequest)
            {
                if (isProduction)
                    context.Result = new JsonResult(new CustomWebResult(title: "Error", content: $"Unexpected error occurred!<br/> Error Code: {logGuid}", resultState: ResultState.Error));
                else
                    context.Result = new JsonResult(new CustomWebResult(title: "Error", content: contentBody, resultState: ResultState.Error));
            }
            else
                CreateErrorViewResult(context, contentBody, logGuid);

            context.Exception.Data.Add(nameof(logGuid), logGuid);

            var sysLog = new NetCoreStack.Mvc.Types.SystemLog()
            {
                ObjectState = ObjectState.Added,
                CreatedDate = DateTime.Now,
                Message = contentBody,
                Category = "NetCoreStackExceptionFilter",
                Level = (int)LogLevel.Critical,
                Ip = GetIp(context),
                ErrorCode = logGuid
            };

            // supress internal server error
            context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
        }
    }
}