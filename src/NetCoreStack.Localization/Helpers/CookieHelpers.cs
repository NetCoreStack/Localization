using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Linq;

namespace NetCoreStack.Localization.Helpers
{
    public static class CookieHelpers
    {
        public static short LocalizationCurrentCulture = 0;
        public static short LocalizationCurrentUICulture = 1;
        public static short LocalizationTimeZone = 2;

        public static string[] ParseLocalizationCookie(string cookieValue)
        {
            if (cookieValue != null)
                return cookieValue.Split('|').Select(o => o.Split('=')[1]).ToArray();

            return null;
        }

        public static CultureInfo GetDefaultLocalizationCookie(this IRequestCookieCollection cookieCollection)
        {
            if (cookieCollection.TryGetValue(CookieRequestCultureProvider.DefaultCookieName, out string requestCultureName))
            {
                string[] values = ParseLocalizationCookie(requestCultureName);
                return new CultureInfo(values[LocalizationCurrentUICulture]);
            }
            return CultureInfo.CurrentUICulture;
        }
    }
}