using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDbServiceLibrary.Models;
using CurrencyClient = MongoDbServiceLibrary.Clients.CurrencyClient;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace MongoDbServiceLibrary
{
    public class MongoDbService : IMongoDbService
    {
        private static string CurrentDataCurrencyName => "C_D_C";
        private static string CurrentDataCurrencyCollectionName => "Current_Date_Currency";

        private readonly IMongoOnlyService _mongoService;
    
        public MongoDbService(IMongoOnlyService mongoOnlyService)
        {
            _mongoService = mongoOnlyService;
        }

        public List<CurrentDateCurrencyModel> GetCurrencyDataFromDb()
        {
            return _mongoService
                .GetDataFromCollection<CurrentDateCurrencyModel>(
                    CurrentDataCurrencyName,
                    CurrentDataCurrencyCollectionName,
                    item => item.Id >= 0
                );
        }
    
        public async Task DataBaseRefresh()
        {
            using HttpResponseMessage response = await CurrencyClient.Client.GetAsync("NBUStatService/v1/statdirectory/exchangenew?json");
            dynamic responseData = response.Content.ReadAsStringAsync().Result;
            List<CurrentDateCurrencyModel> data = JsonConvert.DeserializeObject<List<CurrentDateCurrencyModel>>(responseData);
            if (response.IsSuccessStatusCode)
            {
                await _mongoService
                    .ClearDbCollection<CurrentDateCurrencyModel>(CurrentDataCurrencyName, CurrentDataCurrencyCollectionName);
                await _mongoService
                    .RefillCollection(CurrentDataCurrencyName, CurrentDataCurrencyCollectionName, data);
            }  
        }
    }

    public interface IMongoDbService
    {
        public List<CurrentDateCurrencyModel> GetCurrencyDataFromDb();
        public Task DataBaseRefresh();
    }
}

