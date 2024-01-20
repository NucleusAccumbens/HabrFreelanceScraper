using Scraper.Interfaces;

namespace Scraper.Settings;

public class FreelancehuntParserSettings : IParserSettings
{
    private readonly string _baseUrl = "https://freelancehunt.com/";

    private readonly string _postfix = "projects";

    public string BaseUrl => _baseUrl;

    public string Postfix => _postfix;

    public string GetFullUrl()
    {
        return _baseUrl + _postfix;
    }
}
