using Scraper.Interfaces;

namespace Scraper.Settings;

public class HabrFreelanceParserSettings : IParserSettings
{
    private readonly string _baseUrl = "https://freelance.habr.com/tasks?q=";

    private readonly string _keywords;
    
    public HabrFreelanceParserSettings(string keywords)
    {
        _keywords = keywords;
    }

    public string BaseUrl => _baseUrl;

    public string Postfix => GetPostfix();

    public string GetFullUrl()
    {
        return BaseUrl + Postfix;
    }

    private string GetPostfix()
    {
        if (_keywords.Contains(' '))
        {
            return _keywords.Replace(" ", "+") + "&categories=development_all_inclusive,development_backend,development_frontend,development_prototyping,development_ios,development_android,development_desktop,development_bots,development_games,development_1c_dev,development_scripts,development_voice_interfaces,development_other";
        }

        else return _keywords + "&categories=development_all_inclusive,development_backend,development_frontend,development_prototyping,development_ios,development_android,development_desktop,development_bots,development_games,development_1c_dev,development_scripts,development_voice_interfaces,development_other";
    }
}
