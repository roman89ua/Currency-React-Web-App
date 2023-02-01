using System.Globalization;
using System.Linq.Expressions;
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

        public List<CurrentDateCurrencyModel> GetCurrencyDataFromDb(Expression<Func<CurrentDateCurrencyModel, bool>> predicate )
        {
            return _mongoService
                .GetDataFromCollection<CurrentDateCurrencyModel>(
                    CurrentDataCurrencyName,
                    CurrentDataCurrencyCollectionName
                ).Where(predicate)
                .Select(item => item)
                .ToList();;
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

        public async Task<List<OneCurrencyByDates>> GetSingleCurrencyByDates(string startDate, string endDate, string currencyCode)
        {
            string url = ($"NBU_Exchange/exchange_site?start={DateTransformer(startDate)}&end={DateTransformer(endDate)}&valcode={currencyCode}&sort=exchangedate&order=desc&json");
            Console.WriteLine(url);
            using HttpResponseMessage response = await CurrencyClient.Client.GetAsync(url);
            List<OneCurrencyByDates> data = new List<OneCurrencyByDates>();
            if (response.IsSuccessStatusCode)
            {
                dynamic responseData = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<OneCurrencyByDates>>(responseData);
            }
            return data;
        }

        private string DateTransformer(string date)
        {
            DateTime dateObject = DateTime.Parse(date);
            string dateTransformed = dateObject.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            return dateTransformed;
        }
        
    }
    
}

