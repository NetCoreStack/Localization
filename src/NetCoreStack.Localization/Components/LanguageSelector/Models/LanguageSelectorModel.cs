using System.Collections.Generic;

namespace NetCoreStack.Localization.Components.LanguageSelector.Models
{
    public class LanguageSelectorModel
    {
        public string Name { get; set; }
        public bool SetCookieWithJavaScript { get; set; }

        public List<LanguageViewModel> Languages { get; set; }
    }
}