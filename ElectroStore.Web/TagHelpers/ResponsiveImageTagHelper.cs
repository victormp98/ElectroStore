using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ElectroStore.Web.TagHelpers
{
    [HtmlTargetElement("responsive-image")]
    public class ResponsiveImageTagHelper : TagHelper
    {
        public string Src { get; set; }
        public string Alt { get; set; }
        public string Class { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "picture";
            output.TagMode = TagMode.StartTagAndEndTag;

            var mobileSrc = Src.Replace(".", "-mobile.");
            var tabletSrc = Src.Replace(".", "-tablet.");

            output.Content.AppendHtml($"<source media='(max-width: 576px)' srcset='{mobileSrc}'>");
            output.Content.AppendHtml($"<source media='(max-width: 992px)' srcset='{tabletSrc}'>");
            output.Content.AppendHtml($"<img src='{Src}' alt='{Alt}' class='{Class} img-fluid'>");
        }
    }
}
