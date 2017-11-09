using System.Linq;
using System.Text;
using DotnetPiper.Services;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DotnetPiper.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "article-*",ParentTag="h4")]
    public class ArticlesPostTagHelper : TagHelper
    {
        private TagHelperAttribute propName;

        public string ArticleCategory { get; set; }
        public int ArticleCount { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.TagName = "div";
            output.Attributes.Add("class", "ArticlesPost");

            using (ArticlesService db = new ArticlesService())
            {
                context.AllAttributes.TryGetAttribute("article-category", out propName);
                var query = (from p in db.GetRecentArticles()
                    where p.Category == propName.Value.ToString()
                    orderby p.PublishedDate descending
                    select p
                );

                StringBuilder sb = new StringBuilder();
                foreach (var item in query)
                {
                    sb.AppendFormat("<h3><a href='/home/displayArticle/{0}'>{1}</a></h3>",item.ArticleId, item.Title);
                }
                output.Content.SetHtmlContent(sb.ToString());
            }
        }
    }
}
