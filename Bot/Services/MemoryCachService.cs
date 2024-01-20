using Microsoft.Extensions.Caching.Memory;

namespace Bot.Common.Services;

public class MemoryCachService : IMemoryCachService
{
    private readonly IMemoryCache _memoryCach;

    public MemoryCachService(IMemoryCache memoryCache)
    {
        _memoryCach = memoryCache;
    }

    public string? GetCommandStateFromMemoryCach(long chatId)
    {
        var result = _memoryCach.Get(chatId);

        if (result is not null and string)
        {
            return (string)result;
        }

        else return null;
    }


    public void SetMemoryCach(long chatId, string commandState)
    {
        _memoryCach.Set(chatId, commandState,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            });
    }
}
