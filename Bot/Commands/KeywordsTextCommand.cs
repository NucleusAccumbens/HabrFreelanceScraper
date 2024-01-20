using Bot.Common.Abstractions;
using Bot.Common.Services;
using Scraper.Services;

namespace Bot.Commands;

public class KeywordsTextCommand : BaseTextCommand
{
    private HabrFreelanceParseService _parseService;
    
    private readonly IMemoryCachService _memoryCachService;

    public KeywordsTextCommand(IMemoryCachService memoryCachService)
    {
        _memoryCachService = memoryCachService;
    }

    public override string Name => "keywords";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            string keywords = update.Message.Text;

            _parseService = new(keywords);

            string[] titles = _parseService.GetTitles();

            string[] prices = _parseService.GetPrices();

            string[] links = _parseService.GetLinks();

            for (int i = 0; i < titles.Length; i++) 
            {
                await MessageService.SendMessage(chatId, client,
                $"<a href=\"{links[i]}\"><b>{titles[i]}</b></a>\n\n" +
                $"{prices[i * 2]}", null); 
            }

            _memoryCachService.SetMemoryCach(chatId, String.Empty);

            await MessageService.SendMessage(chatId, client,
                "чтобы осуществить поиск по другим ключевым словам, нажми /start", null);
        }
    }
}
