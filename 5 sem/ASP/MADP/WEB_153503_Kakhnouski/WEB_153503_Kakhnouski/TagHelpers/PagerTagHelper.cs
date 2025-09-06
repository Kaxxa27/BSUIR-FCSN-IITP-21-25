using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WEB_153503_Kakhnouski.TagHelpers;

public class PagerTagHelper : TagHelper
{

    private readonly LinkGenerator _linkGenerator;
    private readonly HttpContext _httpContext;
    public PagerTagHelper(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
    {
        _linkGenerator = linkGenerator;
        _httpContext = httpContextAccessor.HttpContext!;
    }

    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public string? Category { get; set; }
    public bool Admin { get; set; }

    private string? GetUrl(int pageNo)
    {
        RouteValueDictionary values = Admin switch
        {
            true => new()
            {
                 { "pageNo", pageNo }
            },
            _ => new()
            {
                { "category", Category },
                { "pageNo", pageNo }
            },
        };

        return _linkGenerator.GetPathByPage(_httpContext, values: values);
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (TotalPages <= 1)
        {
            output.SuppressOutput();
            return;
        }

        int prevNo = CurrentPage == 1 ? 1 : CurrentPage - 1;
        int nextNo = CurrentPage == TotalPages ? TotalPages : CurrentPage + 1;

        output.TagName = "nav";
        output.Attributes.SetAttribute("aria-label", "Page navigation");

        var ulRoot = new TagBuilder("ul");
        ulRoot.AddCssClass("pagination");

        output.Content.AppendHtml(ulRoot);

        // Previous page button.
        var liPrevious = new TagBuilder("li");
        liPrevious.AddCssClass("page-item");
        if (CurrentPage == 1)
            liPrevious.AddCssClass("disabled");

        var aPrevious = new TagBuilder("a");
        aPrevious.AddCssClass("page-link");
        aPrevious.Attributes.Add("href", GetUrl(prevNo));
        aPrevious.InnerHtml.Append("Назад");

        liPrevious.InnerHtml.AppendHtml(aPrevious);
        ulRoot.InnerHtml.AppendHtml(liPrevious);

        // Page numbers.
        for (int i = 1; i <= TotalPages; i++)
        {
            var li = new TagBuilder("li");
            li.AddCssClass("page-item");
            if (i == CurrentPage)
                li.AddCssClass("active");

            var a = new TagBuilder("a");
            a.AddCssClass("page-link");
            a.Attributes.Add("href", GetUrl(i));
            a.InnerHtml.Append($"{i}");

            li.InnerHtml.AppendHtml(a);
            ulRoot.InnerHtml.AppendHtml(li);
        }

        // Next page button.
        var liNext = new TagBuilder("li");
        liNext.AddCssClass("page-item");
        if (CurrentPage == TotalPages)
            liNext.AddCssClass("disabled");

        var aNext = new TagBuilder("a");
        aNext.AddCssClass("page-link");
        aNext.Attributes.Add("href", GetUrl(nextNo));
        aNext.InnerHtml.Append("Вперед");

        liNext.InnerHtml.AppendHtml(aNext);
        ulRoot.InnerHtml.AppendHtml(liNext);
    }

}
