using MongoDB.Driver;
using MongoDbServiceLibrary.Models;
using CurrencyClient = MongoDbServiceLibrary.Clients.CurrencyClient;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace MongoDbServiceLibrary
{
    public class MongoDbService : IMongoDbService
    {
        private static string CurrentDataCurrencyName => "C_D_C";

        private readonly MongoClientBase _mClient;
    
        public MongoDbService(MongoClientBase mongoClient)
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
    
        public async Task DataBaseRefresh()
        {
            using HttpResponseMessage response = await CurrencyClient.Client.GetAsync("NBUStatService/v1/statdirectory/exchangenew?json");
            dynamic responseData = response.Content.ReadAsStringAsync().Result;
            List<CurrentDateCurrencyModel> data = JsonConvert.DeserializeObject<List<CurrentDateCurrencyModel>>(responseData);
            if (data.Count > 0)
            {
                await GetCurrentDateCurrencyCollection().DeleteManyAsync(item => item.Id >= 0);
                await GetCurrentDateCurrencyCollection().InsertManyAsync(data);
            }  
        }
    }

    public interface IMongoDbService
    {
        public MongoCollectionBase<CurrentDateCurrencyModel> GetCurrentDateCurrencyCollection();

        public List<CurrentDateCurrencyModel> GetCurrencyDataFromDb();

        public Task DataBaseRefresh();
    }
}

