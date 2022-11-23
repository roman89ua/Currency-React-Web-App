namespace Currency_React_Web_App.Interfaces;

public interface ICurrentDateCurrencyCache
{
    public List<T> GetMemoryCache<T>(Func<List<T>> dataGetMethod, string key);
}