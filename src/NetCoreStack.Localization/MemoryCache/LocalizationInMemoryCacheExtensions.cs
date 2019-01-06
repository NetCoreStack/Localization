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

        #region [ByCulture]

        public static List<Resource> GetResourceByLanguageCultureName(this LocalizationInMemoryCacheProvider cache, string cultureName)
        {
            List<Resource> resources = null;
            var languageByCultureName = cache.GetLanguage(cultureName);
            if (languageByCultureName != null)
            {
                resources = cache.GetList<Resource>().Where(k => k.LanguageId == languageByCultureName.Id).ToList();
                return resources;
            }

            resources = cache.GetList<Resource>().Where(k => k.LanguageId == cache.DefaultLanguage.Id).ToList();
            return resources;
        }

        public static Resource GetResourceByLanguageCultureNameAndResourceKey(this LocalizationInMemoryCacheProvider cache, string cultureName, string resourceKey)
        {
            var resourcesByCulture = cache.GetResourceByLanguageCultureName(cultureName);
            return resourcesByCulture.FirstOrDefault(k => k.Key == resourceKey);
        }

        public static string GetResourceValueByLanguageCultureNameAndResourceKey(this LocalizationInMemoryCacheProvider cache, string cultureName, string resourceKey)
        {
            var resource = cache.GetResourceByLanguageCultureNameAndResourceKey(cultureName, resourceKey);
            return resource?.Value;
        }

        #endregion [ByCulture]

        #region [DefaultLanguage]

        public static List<Resource> GetDefaultLanguageResourceList(this LocalizationInMemoryCacheProvider cache)
        {
            var resources = cache.GetResourceListByLanguageId(cache.DefaultLanguage.Id);
            return resources;
        }

        public static Resource GetDefaultLanguageResourceByResourceKey(this LocalizationInMemoryCacheProvider cache, string resourceKey)
        {
            var resource = cache.GetDefaultLanguageResourceList();
            return resource.FirstOrDefault(k => k.Key == resourceKey);
        }

        public static string GetDefaultLanguageResourceValueByResourceKey(this LocalizationInMemoryCacheProvider cache, string resourceKey)
        {
            var resource = cache.GetDefaultLanguageResourceByResourceKey(resourceKey);
            return resource?.Value;
        }

        #endregion [DefaultLanguage]
    }
}