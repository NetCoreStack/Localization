using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetCoreStack.Localization.Interfaces;
using NetCoreStack.Localization.MemoryCache;
using System.Threading.Tasks;

namespace NetCoreStack.Localization.Components
{
    [HtmlTargetElement("netcorestack-languageSelector-scripts")]
    public class NetCoreStackLanguageSelectorScripts : TagHelper, ITagBuilderBase
    {
        private readonly IHtmlHelper _helper;

        public NetCoreStackLanguageSelectorScripts(IHtmlHelper helper, LocalizationInMemoryCacheProvider cacheProvider)
        {
            _helper = helper;
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            (_helper as IViewContextAware).Contextualize(ViewContext);
            output.TagMode = TagMode.SelfClosing;
            output.TagName = "";

            var partialView = "~/Components/LanguageSelector/Views/_LanguageSelectorScripts.cshtml";
            var content = await _helper.PartialAsync(partialView);
            output.Content.SetHtmlContent(content);
        }
    }
}