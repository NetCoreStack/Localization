using NetCoreStack.Localization.Models;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreStack.Localization.MemoryCache
{
    public static class LocalizationInMemoryCacheExtensions
    {
        public static List<Resource> GetResourceListByLanguageId(this LocalizationInMemoryCacheProvider cache, long languageId)
        {
            var resources = cache.GetList<Resource>().Where(k => k.LanguageId == languageId).ToList();
            return resources;
        }

        public static List<Resource> GetResourceByLanguageCultureName(this LocalizationInMemoryCacheProvider cache, string cultureName)
        {
            var languages = cache.GetAllLanguage();
            var languageByCultureName = languages.FirstOrDefault(k => k.CultureName == cultureName);
            if (languageByCultureName != null)
            {
                var resources = cache.GetList<Resource>().Where(k => k.LanguageId == languageByCultureName.Id).ToList();
                return resources;
            }

            var getDefaultLangugae = languages.FirstOrDefault(k => k.IsDefaultLanguage);
            if (getDefaultLangugae != null)
            {
                var resources = cache.GetList<Resource>().Where(k => k.LanguageId == getDefaultLangugae.Id).ToList();
                return resources;
            }

            return new List<Resource>();
        }

        public static Resource GetResourceByLanguageCultureNameAndResourceKey(this LocalizationInMemoryCacheProvider cache, string cultureName, string resourceKey)
        {
            var resourcesByCulture = GetResourceByLanguageCultureName(cache, cultureName);
            return resourcesByCulture.FirstOrDefault(k => k.Key == resourceKey);
        }

        public static string GetResourceValueByLanguageCultureNameAndResourceKey(this LocalizationInMemoryCacheProvider cache, string cultureName, string resourceKey)
        {
            var resource = GetResourceByLanguageCultureNameAndResourceKey(cache, cultureName, resourceKey);
            return resource?.Value;
        }
    }
}