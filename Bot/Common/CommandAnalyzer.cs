using Bot.Common.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.Common;

public class CommandAnalyzer : ICommandAnalyzer
{
    private readonly List<BaseTextCommand> _baseTextCommands;

    private readonly List<BaseCallbackCommand> _baseCallbackCommands;

    private readonly IMemoryCachService _memoryCachService;

    public CommandAnalyzer(IServiceProvider serviceProvider, IMemoryCachService memoryCachService)
    {
        _baseTextCommands = serviceProvider.GetServices<BaseTextCommand>().ToList();
        _baseCallbackCommands = serviceProvider.GetServices<BaseCallbackCommand>().ToList();
        _memoryCachService = memoryCachService;
    }

    public async Task AnalyzeCommandsAsync(ITelegramBotClient client, Update update)
    {
        try
        {            
            if (update.Type == UpdateType.CallbackQuery && update.CallbackQuery != null)
            {
                await AnalyzeCallbackCommand(client, update);
                await client.AnswerCallbackQueryAsync(update.CallbackQuery.Id);
            }
            if (update.Message != null && update.Message.Type == MessageType.Text ||
                update.Message != null && update.Message.Type == MessageType.Photo)
            {
                await AnalyzeTextCommand(client, update);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    private async Task AnalyzeTextCommand(ITelegramBotClient client, Update update)
    {
        if (update.Message != null)
        {
            long chatId = update.Message.Chat.Id;

            Console.WriteLine($"Получено сообщение \"{update.Message.Text}\" " +
                $"от пользователя №{chatId} username {update.Message.Chat.Username}");

            foreach (var command in _baseTextCommands)
            {
                if (command.Name == update.Message?.Text ||
                    _memoryCachService.GetCommandStateFromMemoryCach(chatId) != null && _memoryCachService.GetCommandStateFromMemoryCach(chatId).Contains(command.Name))
                {
                    await command.Execute(update, client);
                    return;
                }
            }
        }
    }

    private async Task AnalyzeCallbackCommand(ITelegramBotClient client, Update update)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Data != null && update.CallbackQuery.Message != null)
        {
            Console.WriteLine($"Получена команда \"{update.CallbackQuery.Data}\" " +
                $"от пользователя №{update.CallbackQuery.Message.Chat.Id} username {update.CallbackQuery.Message.Chat.Username}");

            foreach (var command in _baseCallbackCommands)
            {
                if (command.Contains(update.CallbackQuery))
                {
                    await command.CallbackExecute(update, client);
                }
            }
        }
    }
}
