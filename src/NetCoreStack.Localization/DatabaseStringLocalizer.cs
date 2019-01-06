using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using NetCoreStack.Localization.MemoryCache;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace NetCoreStack.Localization
{
    public class DatabaseStringLocalizer : IStringLocalizer
    {
        private readonly LocalizationInMemoryCacheProvider _cacheProvider;
        private readonly LocalizationSettings _localizationSettings;

        protected CultureInfo CurrentCulture => System.Threading.Thread.CurrentThread.CurrentCulture;

        public DatabaseStringLocalizer(LocalizationInMemoryCacheProvider cacheProvider, IOptions<LocalizationSettings> localizationSettings)
        {
            _cacheProvider = cacheProvider;
            _localizationSettings = localizationSettings.Value;
        }

        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value ?? name, resourceNotFound: value == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = GetString(name);
                var value = string.Format(format ?? name, arguments);
                return new LocalizedString(name, value, resourceNotFound: format == null);
            }
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            CultureInfo.DefaultThreadCurrentCulture = culture;
            return this;
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeAncestorCultures)
        {
            var query = _cacheProvider.ResourceDictionary
                .Where(k => k.Value.Language.CultureName == CurrentCulture.Name)
                .Select(r => new LocalizedString(r.Value.Key, r.Value.Value, true));

            return query;
        }

        private string GetString(string name)
        {
            var value = _cacheProvider.GetResourceValueByLanguageCultureNameAndResourceKey(CurrentCulture.Name, name);

            if (_localizationSettings.UseDefaultLanguageWhenValueIsNull && string.IsNullOrEmpty(value))
                value = _cacheProvider.GetDefaultLanguageResourceValueByResourceKey(name);

            return value;
        }
    }
}