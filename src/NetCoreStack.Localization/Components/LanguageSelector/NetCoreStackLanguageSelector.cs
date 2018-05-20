using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetCoreStack.Localization.Components.LanguageSelector.Models;
using NetCoreStack.Localization.Interfaces;
using NetCoreStack.Localization.MemoryCache;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace NetCoreStack.Localization.Components
{
    [HtmlTargetElement("netcorestack-languageSelector")]
    public class NetCoreStackLanguageSelector : TagHelper, ITagBuilderBase
    {
        private readonly IHtmlHelper _helper;
        private readonly LocalizationInMemoryCacheProvider _cacheProvider;

        public NetCoreStackLanguageSelector(IHtmlHelper helper, LocalizationInMemoryCacheProvider cacheProvider)
        {
            _helper = helper;
            _cacheProvider = cacheProvider;
        }

        public string Name { get; set; }
        public bool SetCookieWithJavaScript { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var languageRepo = _cacheProvider.GetAllLanguage();
            (_helper as IViewContextAware).Contextualize(ViewContext);
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            var viewModel = new LanguageSelectorModel()
            {
                Name = Name,
                Languages = new List<LanguageViewModel>(),
                SetCookieWithJavaScript = SetCookieWithJavaScript
            };

            foreach (var language in languageRepo)
            {
                viewModel.Languages.Add(new LanguageViewModel
                {
                    CultureName = language.CultureName,
                    DisplayName = language.DisplayName,
                    IsSelected = CultureInfo.CurrentUICulture.DisplayName == language.CultureName,
                });
            }

            var partialView = "~/Components/LanguageSelector/Views/_LanguageSelectorContainer.cshtml";
            var content = await _helper.PartialAsync(partialView, viewModel);
            output.Content.SetHtmlContent(content);
        }
    }
}