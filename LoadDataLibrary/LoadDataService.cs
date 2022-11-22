using LoadDataLibrary.Interfaces;
using LoadDataLibrary.Models;
using CurrencyClient = LoadDataLibrary.Clients.CurrencyClient;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace LoadDataLibrary
{
    public class LoadDataService : ILoadDataService
    {
        private static string CurrentDataCurrencyName => "C_D_C";
        private static string CurrentDataCurrencyCollectionName => "Current_Date_Currency";

        private readonly IMongoService _mongoService;
    
        public LoadDataService(IMongoService mongoService)
        {
            _mongoService = mongoService;
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
            if (response.IsSuccessStatusCode)
            {
                dynamic responseData = response.Content.ReadAsStringAsync().Result;
                List<CurrentDateCurrencyModel> data = JsonConvert.DeserializeObject<List<CurrentDateCurrencyModel>>(responseData);
                await _mongoService
                    .ClearDbCollection<CurrentDateCurrencyModel>(CurrentDataCurrencyName, CurrentDataCurrencyCollectionName);
                await _mongoService
                    .RefillCollection(CurrentDataCurrencyName, CurrentDataCurrencyCollectionName, data);
            }  
        }
    }
    
}

