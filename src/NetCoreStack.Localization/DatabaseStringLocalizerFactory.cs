using Microsoft.Extensions.Localization;
using NetCoreStack.Localization.MemoryCache;
using System;

namespace NetCoreStack.Localization
{
    public class DatabaseStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly LocalizationInMemoryCacheProvider _cacheProvider;

        public DatabaseStringLocalizerFactory(LocalizationInMemoryCacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new DatabaseStringLocalizer(_cacheProvider);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new DatabaseStringLocalizer(_cacheProvider);
        }
    }
}