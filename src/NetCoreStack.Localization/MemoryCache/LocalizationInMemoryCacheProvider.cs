using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NetCoreStack.Contracts;
using NetCoreStack.Localization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreStack.Localization.MemoryCache
{
    public class LocalizationInMemoryCacheProvider
    {
        private readonly IMemoryCache _memoryCache;
        private readonly List<string> _cachedItems;
        private readonly IServiceProvider _serviceProvider;

        public string InctanceId { get; set; }

        public LocalizationInMemoryCacheProvider(IMemoryCache memoryCache, IServiceProvider serviceProvider)
        {
            _memoryCache = memoryCache;
            _cachedItems = new List<string>();
            InctanceId = Guid.NewGuid().ToString("N");
            _serviceProvider = serviceProvider;
        }

        private async Task SetKey(string key, LocalizationCacheProviderOptions cacheProviderOption = null)
        {
            object value = null;
            using (var unitOfWork = _serviceProvider.CreateUnitOfWork())
            {
                switch (key)
                {
                    case nameof(Language):
                        value = unitOfWork.Repository<Language>().ToList();
                        break;

                    case nameof(Resource):
                        value = unitOfWork.Repository<Resource>().Include(k => k.Language).ToList();
                        break;
                }

                var defaultLocalizationCacheProviderOptions = new LocalizationCacheProviderOptions
                {
                    Priority = CacheItemPriority.NeverRemove,
                    AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddMinutes(1))
                };

                SetObject(key, value, cacheProviderOption ?? defaultLocalizationCacheProviderOptions);
            }

            await Task.CompletedTask;
        }

        public object ReSetKey(object value, string cacheName)
        {
            if (value == null)
            {
                if (_cachedItems.Any(k => k == cacheName))
                    _cachedItems.Remove(cacheName);

                Task.Run(() => SetKey(cacheName)).GetAwaiter().GetResult();
                value = _memoryCache.Get(cacheName);
            }
            return value;
        }

        public List<TEntity> GetList<TEntity>()
        {
            var cacheName = typeof(TEntity).Name;
            var value = ReSetKey(_memoryCache.Get(cacheName), cacheName);
            return (List<TEntity>)value;
        }

        public object SetObject(string key, object value, LocalizationCacheProviderOptions options)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException("Cache value is null");
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var entryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = options.AbsoluteExpiration,
                Priority = options.Priority,
                SlidingExpiration = options.SlidingExpiration
            };

            _memoryCache.Remove(key);
            _memoryCache.Set(key, value, entryOptions);
            _cachedItems.Add(key);

            return value;
        }

        public object GetObject(string key)
        {
            return ReSetKey(_memoryCache.Get(key), key);
        }

        public T GetObject<T>(string key)
        {
            return (T)GetObject(key);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public TEntity GetItem<TEntity>(long id) where TEntity : IModelKey<long>
        {
            return GetItem((TEntity e) => e.Id == id);
        }

        public TEntity GetItem<TEntity>(Func<TEntity, bool> idSelector) where TEntity : IModelKey<long>
        {
            var list = GetList<TEntity>();
            return list.SingleOrDefault(idSelector);
        }




        #region [EntityCacheHelper]

        public List<Language> GetAllLanguage()
        {
            return GetList<Language>();
        }

        private IReadOnlyDictionary<long, Resource> _resourceDictionary;

        public IReadOnlyDictionary<long, Resource> ResourceDictionary
        {
            get
            {
                if (_resourceDictionary == null)
                {
                    _resourceDictionary = GetList<Resource>().ToDictionary(x => x.Id, x => x);
                }

                return _resourceDictionary;
            }
            set
            {
                _resourceDictionary = value;
            }
        }

        #endregion [EntityCacheHelper]
    }
}