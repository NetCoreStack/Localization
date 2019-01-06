using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using NetCoreStack.Localization.MemoryCache;
using System;

namespace NetCoreStack.Localization
{
    public class DatabaseStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly LocalizationInMemoryCacheProvider _cacheProvider;
        private readonly IOptions<LocalizationSettings> _localizationSettings;

        public DatabaseStringLocalizerFactory(LocalizationInMemoryCacheProvider cacheProvider, IOptions<LocalizationSettings> localizationSettings)
        {
            _cacheProvider = cacheProvider;
            _localizationSettings = localizationSettings;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new DatabaseStringLocalizer(_cacheProvider, _localizationSettings);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new DatabaseStringLocalizer(_cacheProvider, _localizationSettings);
        }
    }
}