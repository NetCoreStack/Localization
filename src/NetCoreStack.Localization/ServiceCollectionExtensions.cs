using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using NetCoreStack.Data;
using NetCoreStack.Localization.Components;
using NetCoreStack.Localization.Interfaces;
using NetCoreStack.Localization.MemoryCache;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NetCoreStack.Localization
{
    public static class ServiceCollectionExtensions
    {
        public static void AddNetCoreStackLocalization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddNetCoreStackSqlDb<LocalizationDBContext>(configuration);

            services.AddSingleton<IConfigureOptions<MvcOptions>, DefaultDataAnnotationsMvcOptionsSetup>();
            services.AddSingleton<LocalizationInMemoryCacheProvider>();

            services.AddSingleton<IStringLocalizerFactory, DatabaseStringLocalizerFactory>();
            services.AddSingleton<ILocalizationStartupTask, DatabaseStartupTask>();
            services.AddSingleton<IConfigureOptions<MvcOptions>, DefaultDataAnnotationsMvcOptionsSetup>();

            services.AddScoped<IStringLocalizer, DatabaseStringLocalizer>();

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProviders.Add(new CompositeFileProvider(new EmbeddedFileProvider(typeof(NetCoreStackLanguageSelector).GetTypeInfo().Assembly)));
            });

            services.AddMvc()
                .ConfigureApplicationPartManager(k => k.FeatureProviders.Add(new GenericControllerFeatureProvider())) // OR //.AddApplicationPart(typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly)
                .AddDataAnnotationsLocalization()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public static void UseNetCoreStackLocalization(this IApplicationBuilder app)
        {
            Task.Run(() => LocalizationClientHostingFactory.Instance.Bootstrap(app)).GetAwaiter().GetResult();

            var cacheProvider = app.ApplicationServices.GetService<LocalizationInMemoryCacheProvider>();
            var supportedCultures = new List<CultureInfo>();
            RequestCulture defaultCulture = null;
            var languageRepo = cacheProvider.GetAllLanguage();
            foreach (var language in languageRepo)
            {
                if (defaultCulture == null && language.IsDefaultLanguage)
                    defaultCulture = new RequestCulture(language.CultureName);

                supportedCultures.Add(new CultureInfo(language.CultureName));
            }

            var requestLocalizationOptions = new RequestLocalizationOptions();
            requestLocalizationOptions.DefaultRequestCulture = defaultCulture;
            requestLocalizationOptions.SupportedCultures = supportedCultures;
            requestLocalizationOptions.SupportedUICultures = supportedCultures;

            app.UseRequestLocalization(requestLocalizationOptions);
        }

    public class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var currentComponentController = feature.Controllers
                                     .Where(c => c.AsType().Assembly.FullName == typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly.FullName)
                                     .Select(c => c.AsType());

            foreach (var controller in currentComponentController)
            {
                if (!feature.Controllers.Any(k => k.Equals(controller.GetTypeInfo())))
                    feature.Controllers.Add(controller.GetTypeInfo());
            }

        }
    }
}
}