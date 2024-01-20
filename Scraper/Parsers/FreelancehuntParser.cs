using AngleSharp.Browser;
using AngleSharp.Html.Dom;
using Scraper.Interfaces;

namespace Scraper.Parsers;

public class FreelancehuntParser : IParser<string[]>
{
    private readonly List<string> _titles = new();

    private readonly List<string> _links = new();

    private readonly List<string> _prices = new();

    public string[] ParseLinks(IHtmlDocument document)
    {
        var linksItems = document.Links
            .Where(link => ((IHtmlAnchorElement)link).PathName.Contains("https://freelancehunt.com/project"));

        foreach (var link in linksItems)
        {
            _links.Add(((IHtmlAnchorElement)link).PathName);
        }

        return _links.ToArray();
    }

    public string[] ParseTitles(IHtmlDocument document)
    {
        var titleItems = document.QuerySelectorAll("tr")
            .Where(item => item.ClassName != null && item.ClassName.Contains("featured"));

        foreach (var title in titleItems)
        {
            _titles.Add(title.TextContent);
        }

        return _titles.ToArray();
    }

    public string[] ParsePrices(IHtmlDocument document)
    {
        var priceItems = document.QuerySelectorAll("div")
            .Where(item => item.ClassName != null && item.ClassName.Contains("text-green price with-tooltip"));

        foreach (var price in priceItems)
        {
            _prices.Add(price.TextContent);
        }

        return _prices.ToArray();
    }
}
