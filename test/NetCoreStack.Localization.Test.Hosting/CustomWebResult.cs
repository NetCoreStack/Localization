using NetCoreStack.Mvc;
using NetCoreStack.Mvc.Enums;
using System;
using System.Collections.Generic;

namespace NetCoreStack.Localization.Test.Hosting
{
    public class CustomWebResult : WebResult
    {
        public CustomWebResult(string title = "", string content = "", ResultState resultState = NetCoreStack.Mvc.ResultState.Success, ResultPosition position = ResultPosition.TopFullWidth, int duration = 5000, List<ModelValidationResult> validations = null, string redirectUrl = null, bool resetForm = true)
            : base(title, content, resultState, position, duration, validations, redirectUrl, resetForm)
        {
            RedirectUrl = redirectUrl;
            if (title == Globals.Prefix || String.IsNullOrWhiteSpace(title))
            {
                switch (resultState)
                {
                    case NetCoreStack.Mvc.ResultState.Error:
                        title = "Hata!";
                        break;

                    case NetCoreStack.Mvc.ResultState.Info:
                        title = "Bilgilendirme";
                        break;

                    case NetCoreStack.Mvc.ResultState.Success:
                        title = "Bilgilendirme";
                        break;

                    case NetCoreStack.Mvc.ResultState.Warning:
                        title = "Uyarı!";
                        break;
                }
            }

            Title = title;
        }
    }

    public class CustomWebResult<T> : WebResult
    {
        public T Result { get; set; }

        public CustomWebResult(T result = default(T),
            string title = "",
            string content = DefaultSuccessMessage,
            ResultState resultState = NetCoreStack.Mvc.ResultState.Success,
            ResultPosition position = ResultPosition.TopFullWidth,
            int duration = 5000,
            List<ModelValidationResult> validations = null)
            : base(title, content, resultState, position, duration, validations)
        {
            Result = result;
        }
    }
}