using MongoDB.Driver;

namespace Currency_React_Web_App.Services;

public class FetchDataService : IFetchDataService
{
    private static string CurrentDataCurrencyName => "C_D_C";

    private readonly MongoClientBase _mClient;
    
    public FetchDataService(MongoClientBase mongoClient)
    {
        _mClient = mongoClient;
    }

    public MongoCollectionBase<CurrentDateCurrencyModel> GetCurrentDateCurrencyCollection()
    {
        var currentDb = (MongoDatabaseBase)_mClient.GetDatabase("Current_Date_Currency");
        
        return (MongoCollectionBase<CurrentDateCurrencyModel>)currentDb.GetCollection<CurrentDateCurrencyModel>(CurrentDataCurrencyName);
    }

    public List<CurrentDateCurrencyModel> GetCurrencyDataFromDb()
    {
        List<CurrentDateCurrencyModel> data = GetCurrentDateCurrencyCollection().Find(item => item.Id >= 0).ToList();
        return data;
    }
}

public interface IFetchDataService
{
    public MongoCollectionBase<CurrentDateCurrencyModel> GetCurrentDateCurrencyCollection();

    public List<CurrentDateCurrencyModel> GetCurrencyDataFromDb();
}
