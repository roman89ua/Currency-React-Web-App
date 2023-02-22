using System.Linq.Expressions;
using LoadDataLibrary.Models;

namespace LoadDataLibrary.Interfaces;

public interface ILoadDataService
{
    public List<T> GetCurrencyDataFromDb<T>(Expression<Func<T, bool>> predicate );
    public List<T> GetSingleCurrencyDataHistoryFromDb<T>(Expression<Func<T, bool>> predicate, string collectionName);
    public Task DataBaseRefresh();
    public Task LoadCurrenciesHistory(List<CurrentDateCurrencyModel> listOfAllCurrencies);
}