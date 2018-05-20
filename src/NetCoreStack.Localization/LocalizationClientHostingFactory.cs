using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NetCoreStack.Localization.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreStack.Localization
{
    public sealed class LocalizationClientHostingFactory
    {
        private static readonly Lazy<LocalizationClientHostingFactory> lazy = new Lazy<LocalizationClientHostingFactory>(() => new LocalizationClientHostingFactory());

        public static LocalizationClientHostingFactory Instance { get { return lazy.Value; } }

        private LocalizationClientHostingFactory()
        {
        }

        public async Task Bootstrap(IApplicationBuilder app)
        {
            try
            {
                var tasks = app.ApplicationServices.GetServices<ILocalizationStartupTask>();
                if (tasks != null && tasks.Any())
                {
                    foreach (var startupTask in tasks)
                    {
                        await startupTask.InvokeAsync(app);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}