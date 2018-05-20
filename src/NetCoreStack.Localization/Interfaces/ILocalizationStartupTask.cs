using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;

namespace NetCoreStack.Localization.Interfaces
{
    public interface ILocalizationStartupTask
    {
        Task InvokeAsync(IApplicationBuilder app);
    }
}