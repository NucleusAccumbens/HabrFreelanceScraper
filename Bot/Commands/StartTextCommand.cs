using Bot.Common.Abstractions;
using Bot.Common.Services;

namespace Bot.Commands;

public class StartTextCommand : BaseTextCommand
{
    private readonly IMemoryCachService _memoryCachService;

    public StartTextCommand(IMemoryCachService memoryCachService)
    {
        _memoryCachService = memoryCachService;
    }

    public override string Name => "/start";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null)
        {
            long chatId = update.Message.Chat.Id;

            _memoryCachService.SetMemoryCach(chatId, "keywords");

            await MessageService.SendMessage(chatId, client,
                "отправь ключевые слова для поиска проектов сообщением в этот чат, " +
                "и я пришлю тебе подходящие заказы", null);
        }
    }
}
