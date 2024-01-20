using AngleSharp.Html.Parser;
using PriceParser.Services;
using Scraper.Parsers;
using Scraper.Settings;

namespace Scraper.Services;

public class HabrFreelanceParseService
{
    private readonly HabrFreelanceParser _habrParser = new();

    private readonly HtmlParser _htmlParser = new();

    private readonly string _keywords;

    public HabrFreelanceParseService(string keywords)
    {
        _keywords = keywords;
    }

    private string? GetSours()
    {
        var parserSettings = new HabrFreelanceParserSettings(_keywords);

        var loader = new HtmlLoader(parserSettings);

        return loader.GetSourceByUrlAsync().Result;
    }

    public string[] GetTitles()
    {
        var sours = GetSours();

        if (sours != null)
        {
            var document = _htmlParser.ParseDocument(sours);

            return _habrParser.ParseTitles(document);
        }

        return null;
    }

    public string[] GetPrices()
    {
        var sours = GetSours();

        if (sours != null)
        {
            var document = _htmlParser.ParseDocument(sours);

            return _habrParser.ParsePrices(document);
        }

        return null;
    }

    public string[] GetLinks()
    {
        var sours = GetSours();

        if (sours != null)
        {
            var document = _htmlParser.ParseDocument(sours);

            return _habrParser.ParseLinks(document);
        }

        return null;
    }
}
