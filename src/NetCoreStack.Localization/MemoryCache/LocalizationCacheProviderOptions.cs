using Microsoft.Extensions.Caching.Memory;
using System;

namespace NetCoreStack.Localization.MemoryCache
{
    public class LocalizationCacheProviderOptions
    {
        public CacheItemPriority Priority { get; set; }
        public DateTimeOffset? AbsoluteExpiration { get; set; }

        /// <summary>
        ///   Gets or sets how long a cache entry can be inactive (e.g. not accessed) before
        ///   it will be removed. This will not extend the entry lifetime beyond the absolute
        ///   expiration (if set).
        /// </summary>
        public TimeSpan? SlidingExpiration { get; set; }
    }
}