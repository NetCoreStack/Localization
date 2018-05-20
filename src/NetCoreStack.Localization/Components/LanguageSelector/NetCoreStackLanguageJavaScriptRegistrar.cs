using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetCoreStack.Localization.Interfaces;
using NetCoreStack.Localization.MemoryCache;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace NetCoreStack.Localization.Components
{
    [HtmlTargetElement("netcorestack-javascriptregistrar")]
    public class NetCoreStackLanguageJavaScriptRegistrar : TagHelper, ITagBuilderBase
    {
        private readonly IHtmlHelper _helper;
        private readonly LocalizationInMemoryCacheProvider _cacheProvider;

        public NetCoreStackLanguageJavaScriptRegistrar(IHtmlHelper helper, LocalizationInMemoryCacheProvider cacheProvider)
        {
            _helper = helper;
            _cacheProvider = cacheProvider;
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var resources = _cacheProvider.GetResourceByLanguageCultureName(CultureInfo.CurrentUICulture.Name);
            (_helper as IViewContextAware).Contextualize(ViewContext);

            output.TagName = "";
            output.TagMode = TagMode.SelfClosing;

            var viewModel = new Dictionary<string, string>();
            foreach (var resource in resources)
            {
                viewModel.Add(resource.Key, resource.Value);
            }

            var partialView = "~/Components/LanguageSelector/Views/_NetCoreStackLanguageJavaScriptRegistrar.cshtml";
            var content = await _helper.PartialAsync(partialView, viewModel);
            output.Content.SetHtmlContent(content);
        }
    }
}