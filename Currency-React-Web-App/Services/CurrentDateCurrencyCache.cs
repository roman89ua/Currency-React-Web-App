using Currency_React_Web_App.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Currency_React_Web_App.Services;

public class CurrentDateCurrencyCache : ICurrentDateCurrencyCache
{
    private readonly IMemoryCache _memoryCache;
    
    public CurrentDateCurrencyCache(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    private void SetMemoryCache<T>(List<T> newCurrencyList, string key)
    {
        var timerStamp = DateTime.Now;
        var options = new MemoryCacheEntryOptions {
            AbsoluteExpiration = timerStamp.AddMinutes(10),
        };
        _memoryCache.Set(key, newCurrencyList, options);
    }
    
    
    public List<T> GetMemoryCache<T>(Func<List<T>> dataGetMethod, string key)
    {
        if (!_memoryCache.TryGetValue(key, out List<T> currencyList))
        {
            currencyList = dataGetMethod();
            SetMemoryCache(currencyList, key);
        }
        return currencyList;
    }
}

