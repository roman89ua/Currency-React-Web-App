using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.ComponentModel;
using Currency_React_Web_App.Enums;

namespace Currency_React_Web_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyCurrentDateController : ControllerBase
    {
        private MongoClientBase MClient { get; set; }

        private string Url => "NBUStatService/v1/statdirectory/exchangenew?json";

        private string CollectionName => "C_D_C";

        private MongoDatabaseBase CurrentDb { get; set; }

        private MongoCollectionBase<CurrentDateCurrencyModel> Collection { get; set; }


        public CurrencyCurrentDateController(MongoClientBase mongoClient)
        {
            MClient = mongoClient;
            CurrentDb = (MongoDatabaseBase)MClient.GetDatabase("Current_Date_Currency");
            Collection = (MongoCollectionBase<CurrentDateCurrencyModel>)CurrentDb.GetCollection<CurrentDateCurrencyModel>(CollectionName);

        }

        [HttpGet]
        public async Task<List<CurrentDateCurrencyModel>> Get()
        {
            await Collection.DeleteManyAsync(item => item.Id >= 0);
            List<CurrentDateCurrencyModel> data ;

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(Url))
            {
                if (response.IsSuccessStatusCode)
                {
                    dynamic responseData = response.Content.ReadAsStringAsync().Result;
                    data = JsonConvert.DeserializeObject<List<CurrentDateCurrencyModel>>(responseData);
                    var dataFetchResult = data.ToList<CurrentDateCurrencyModel>();
                    await Collection.InsertManyAsync(dataFetchResult);

                    var collectionData = Collection.Find(item => item.Id >= 0).ToList();

                    return collectionData;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        [HttpGet("sortcurrencydata/{key}/{order}")]
        public ActionResult<List<CurrentDateCurrencyModel>> SortCurrencyData(CurrentDateCurrencyEnum key, SortOrder order)
        {
            List<CurrentDateCurrencyModel> collectionData = Collection.Find(item => item.Id >= 0).ToList();

            switch (key)
            {
                case CurrentDateCurrencyEnum.Text:
                    collectionData.Sort((x, y) => (order == SortOrder.ascending)
                        ? String.Compare(x.Text, y.Text, StringComparison.CurrentCulture)
                        : String.Compare(y.Text, x.Text, StringComparison.CurrentCulture));
                    break;

                case CurrentDateCurrencyEnum.Rate:
                    collectionData = order == SortOrder.ascending ? collectionData.OrderBy(collectionItem => collectionItem.Rate).ToList() : collectionData.OrderByDescending(collectionItem => collectionItem.Rate).ToList();
                    break;

                case CurrentDateCurrencyEnum.Currency:
                    collectionData.Sort((x, y) => (order == SortOrder.ascending)
                        ? String.Compare(x.Currency, y.Currency, StringComparison.CurrentCulture)
                        : String.Compare(y.Currency, x.Currency, StringComparison.CurrentCulture)
                    );
                    break;

                case CurrentDateCurrencyEnum.ExchangeDate:
                    collectionData.Sort((x, y) =>  (order == SortOrder.ascending)
                        ? DateTime.Compare(DateTime.Parse(x.ExchangeDate), DateTime.Parse(y.ExchangeDate))
                        : DateTime.Compare(DateTime.Parse(y.ExchangeDate), DateTime.Parse(x.ExchangeDate))
                    );
                    break;
            }

            return collectionData;
        }

        [HttpGet("filtercurrencydata/{value}")]
        public ActionResult<List<CurrentDateCurrencyModel>> FilterCurrencyData(string value)
        {
            List<CurrentDateCurrencyModel> collectionData = Collection.Find(item => item.Id >= 0).ToList();

            if (String.IsNullOrEmpty(value) || value == "null")
            {
                return collectionData;
            }
            else
            {
                string lowerCaseValue = value.ToLower();

                var sortedData = collectionData.
                    Where(item => 
                        item.Text.ToLower().Contains(lowerCaseValue) || item.Currency.ToLower().Contains(lowerCaseValue))
                    .Select(item => item)
                    .ToList();

                return sortedData;
            }

        }
    }
}
