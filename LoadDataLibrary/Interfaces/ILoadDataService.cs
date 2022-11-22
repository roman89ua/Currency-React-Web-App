using LoadDataLibrary.Models;

namespace LoadDataLibrary.Interfaces;

public interface ILoadDataService
{
    public List<CurrentDateCurrencyModel> GetCurrencyDataFromDb();
    public Task DataBaseRefresh();
}