using AngleSharp.Html.Dom;
using Scraper.Interfaces;

namespace Scraper.Parsers;

public class HabrFreelanceParser : IParser<string[]>
{
    private readonly List<string> _titles = new();

    private readonly List<string> _links = new();

    private readonly List<string> _prices = new();

    public string[] ParseTitles(IHtmlDocument document)
    {
        var titleItems = document.QuerySelectorAll("div")
            .Where(item => item.ClassName != null && item.ClassName.Contains("task__title"));

        foreach (var title in titleItems) 
        {
            _titles.Add(title.TextContent);
        }

        return _titles.ToArray();
    }

    public string[] ParseLinks(IHtmlDocument document)
    {
        var linksItems = document.Links
            .Where(link => ((IHtmlAnchorElement)link).PathName.Contains("/tasks/"));

        foreach (var link in linksItems)
        {
            _links.Add("https://freelance.habr.com/" + ((IHtmlAnchorElement)link).PathName);
        }

        return _links.ToArray();
    }

    public string[] ParsePrices(IHtmlDocument document)
    {
        var priceItems = document.QuerySelectorAll("div")
            .Where(item => item.ClassName != null && item.ClassName.Contains("task__price"));

        foreach (var price in priceItems)
        {
            _prices.Add(price.TextContent);
        }

        return _prices.ToArray();
    }
}
