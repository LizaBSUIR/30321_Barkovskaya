using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace _30321_Barkovskaya.UI.TagHelpers
{
    [HtmlTargetElement("pager", Attributes = "current-page, total-pages")]
    public class Pager : TagHelper
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Pager(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
        }

        // номер текущей страницы
        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }
        // общее количество страниц
        [HtmlAttributeName("total-pages")]
        public int TotalPages { get; set; }
        // имя категории объектов
        [HtmlAttributeName("category")]
        public string? Category { get; set; }
        // признак страниц администратора
        [HtmlAttributeName("admin")]
        public bool Admin { get; set; } = false;

        int Prev => CurrentPage == 1 ? 1 : CurrentPage - 1;
        int Next => CurrentPage == TotalPages ? TotalPages : CurrentPage + 1;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddClass("row", HtmlEncoder.Default);
            var nav = new TagBuilder("nav");
            nav.Attributes.Add("aria-label", "pagination");
            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");

            ul.InnerHtml.AppendHtml(CreateListItem(Category, Prev, "<span aria-hidden=\"true\">&laquo;</span>"));

            for (var index = 1; index <= TotalPages; index++)
            {
                ul.InnerHtml.AppendHtml(CreateListItem(Category, index, String.Empty));
            }

            ul.InnerHtml.AppendHtml(CreateListItem(Category, Next, "<span aria-hidden=\"true\">&raquo;</span>"));

            nav.InnerHtml.AppendHtml(ul);
            output.Content.AppendHtml(nav);
        }

        private TagBuilder CreateListItem(string? category, int pageNo, string? innerText)
        {
            var li = new TagBuilder("li");
            li.AddCssClass("page-item");
            if (pageNo == CurrentPage && String.IsNullOrEmpty(innerText))
            {
                li.AddCssClass("active");
            }

            var a = new TagBuilder("a");
            a.AddCssClass("page-link");
            var routeData = new { pageNo = pageNo, category = category };
            string url;

            if (Admin)
            {
                url = _linkGenerator.GetPathByPage(_httpContextAccessor.HttpContext, page: "./Index", values: routeData);
            }
            else
            {
                url = _linkGenerator.GetPathByAction("index", "product", routeData);
            }

            a.Attributes.Add("href", url);
            var text = String.IsNullOrEmpty(innerText) ? pageNo.ToString() : innerText;
            a.InnerHtml.AppendHtml(text);

            li.InnerHtml.AppendHtml(a);

            return li;
        }
    }
}
