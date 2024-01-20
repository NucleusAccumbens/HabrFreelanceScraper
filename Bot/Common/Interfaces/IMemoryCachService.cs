namespace Bot.Common.Interfaces;

public interface IMemoryCachService
{
    void SetMemoryCach(long chatId, string commandState);  

    string? GetCommandStateFromMemoryCach(long chatId);
}
