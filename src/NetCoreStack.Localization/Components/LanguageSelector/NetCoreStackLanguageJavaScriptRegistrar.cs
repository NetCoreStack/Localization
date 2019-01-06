using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using NetCoreStack.Localization.Extension;
using NetCoreStack.Localization.Interfaces;
using NetCoreStack.Localization.MemoryCache;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreStack.Localization.Components
{
    [HtmlTargetElement("netcorestack-javascriptregistrar")]
    public class NetCoreStackLanguageJavaScriptRegistrar : TagHelper, ITagBuilderBase
    {
        private readonly IHtmlHelper _helper;
        private readonly LocalizationInMemoryCacheProvider _cacheProvider;
        private readonly IStringLocalizer _stringLocalizer;

        public NetCoreStackLanguageJavaScriptRegistrar(IHtmlHelper helper, LocalizationInMemoryCacheProvider cacheProvider, IStringLocalizer stringLocalizer)
        {
            _helper = helper;
            _cacheProvider = cacheProvider;
            _stringLocalizer = stringLocalizer;
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var defaultLanguage = _cacheProvider.DefaultLanguage;
            var defaultLangueageResource = _cacheProvider.GetDefaultLanguageResourceList();
            (_helper as IViewContextAware).Contextualize(ViewContext);

            output.TagName = "";
            output.TagMode = TagMode.SelfClosing;

            var viewModel = new Dictionary<string, string>();
            foreach (var resource in defaultLangueageResource)
            {
                if (viewModel.ContainsKey(resource.Key))
                    continue;

                if (defaultLanguage.CultureName != CultureInfo.CurrentUICulture.Name)
                {
                    var localizer = _stringLocalizer[resource.Key];
                    viewModel.Add(localizer.Name, localizer.Value);
                }
                else
                {
                    viewModel.Add(resource.Key, resource.Value);
                }
            }

            if (defaultLanguage.CultureName != CultureInfo.CurrentUICulture.Name)
            {
                var resources = _cacheProvider
                    .GetResourceByLanguageCultureName(CultureInfo.CurrentUICulture.Name)
                    .ToDictionary(k => k.Key, k => k.Value);
                viewModel.AddRangeNewOnly(resources);
            }

            var partialView = "~/Components/LanguageSelector/Views/_NetCoreStackLanguageJavaScriptRegistrar.cshtml";
            var content = await _helper.PartialAsync(partialView, viewModel);
            output.Content.SetHtmlContent(content);
        }
    }
}