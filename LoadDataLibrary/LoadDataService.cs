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
        private static string DbName => "Currency";
        private static string CollectionName => "Current_Date_Currency";

        private const string HistoryStartFrom = "01.01.1990";

        private readonly IMongoService _mongoService;

        public LoadDataService(IMongoService mongoService)
        {
            _mongoService = mongoService;
        }

        public List<T> GetCurrencyDataFromDb<T>(Expression<Func<T, bool>> predicate )
        {
            return _mongoService
                .GetQueryableDataFromCollection<T>(
                    DbName,
                    CollectionName
                ).Where(predicate)
                .Select(item => item)
                .ToList();
        }
        
        public List<T> GetSingleCurrencyDataHistoryFromDb<T>(Expression<Func<T, bool>> predicate, string collectionName)
        {
            return _mongoService
                .GetQueryableDataFromCollection<T>(DbName, collectionName)
                .Where(predicate)
                .Select(item => item)
                .ToList();
        }
    
        public async Task DataBaseRefresh()
        {
            using HttpResponseMessage response = await CurrencyClient.Client.GetAsync("NBUStatService/v1/statdirectory/exchangenew?json");
            if (response.IsSuccessStatusCode)
            {
                dynamic responseData = response.Content.ReadAsStringAsync().Result;
                List<CurrentDateCurrencyModel> data = JsonConvert.DeserializeObject<List<CurrentDateCurrencyModel>>(responseData);
                await _mongoService
                    .ClearDbCollection<CurrentDateCurrencyModel>(DbName, CollectionName);
                await _mongoService
                    .RefillCollection(DbName, CollectionName, data);
            }  
        }

        public async Task LoadCurrenciesHistory(List<CurrentDateCurrencyModel> listOfAllCurrencies)
        {
            var collectionsNames = _mongoService.GetDbCollectionsNameList(DbName);
            
            string format = "s";
            string today = DateTime.Now.ToString(format, CultureInfo.InvariantCulture);
            var sevenDaysBack = DateTime.Today.AddDays(-6).ToString(format, CultureInfo.InvariantCulture);
            
            try
            {
                foreach (var currencyItem in listOfAllCurrencies)
                {
                    if (collectionsNames.Contains(currencyItem.Currency))
                    {
                        await UpdateCollection(sevenDaysBack, today, currencyItem.Currency);
                    }
                    else
                    {
                        await AddNewCollection(HistoryStartFrom, today ,currencyItem.Currency);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error: '{e.Message}' occurred during execution LoadCurrenciesHistory method from LoadDataService class");
                throw;
            }
        }
        
        private async Task<List<OneCurrencyByDatesModel>> GetSingleCurrencyByDates(string startDate, string endDate, string currencyCode)
        {
            string url = ($"NBU_Exchange/exchange_site?start={ApiDateTransformer(startDate)}&end={ApiDateTransformer(endDate)}&valcode={currencyCode}&sort=exchangedate&order=asc&json");
            Console.WriteLine(url);
            using HttpResponseMessage response = await CurrencyClient.Client.GetAsync(url);
            List<OneCurrencyByDatesModel> data = new List<OneCurrencyByDatesModel>();
            if (response.IsSuccessStatusCode)
            {
                dynamic responseData = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<OneCurrencyByDatesModel>>(responseData);
            }
            return data;
        }

        private string ApiDateTransformer(string date)
        {
            DateTime dateObject = DateTime.Parse(date);
            return dateObject.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
        }

        private async Task UpdateCollection(string startDate, string endDate, string currencyName)
        {
            List<OneCurrencyByDatesModel> lastWeekData = await GetSingleCurrencyByDates(startDate, endDate, currencyName);

            foreach (var item in lastWeekData)
            {
                bool existInDb = _mongoService.GetQueryableDataFromCollection<OneCurrencyByDatesModel>(DbName, currencyName).Any(dbCurrency => dbCurrency.ExchangeDate == item.ExchangeDate);

                if (!existInDb) await _mongoService.AddOneToCollection(DbName, currencyName, item);
            }
        }
        
        private async Task AddNewCollection(string startDate, string endDate, string collectionName)
        {
            await _mongoService.CreateDbCollection(DbName, collectionName);
                        
            List<OneCurrencyByDatesModel> wholeCurrentCurrencyHistory =
                await GetSingleCurrencyByDates(startDate, endDate, collectionName);
                        
            await _mongoService.AddManyToCollection(DbName, collectionName, wholeCurrentCurrencyHistory);
        }
    }
    
}

