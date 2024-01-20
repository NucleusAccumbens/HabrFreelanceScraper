using AngleSharp.Html.Dom;

namespace Scraper.Interfaces;

public interface IParser<T> where T : class
{
    T ParseTitles(IHtmlDocument document);
    T ParseLinks(IHtmlDocument document);
}
