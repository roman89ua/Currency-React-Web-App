using System.Linq.Expressions;
using LoadDataLibrary.Models;

namespace LoadDataLibrary.Interfaces;

public interface ILoadDataService
{
    public List<CurrentDateCurrencyModel> GetCurrencyDataFromDb(Expression<Func<CurrentDateCurrencyModel, bool>> predicate );
    public Task DataBaseRefresh();
    public Task<List<OneCurrencyByDates>> GetSingleCurrencyByDates(string startDate, string endDate, string currencyCode);
}