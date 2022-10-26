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
        private MongoClientBase _MClient { get; set; }

        private string _url { get; } = "NBUStatService/v1/statdirectory/exchangenew?json";

        private string _collectinName { get; } = ("C_D_C");

        private MongoDatabaseBase _currentDB { get; set; }

        private MongoCollectionBase<CurrentDateCurrencyModel> _collection { get; set; }


        public CurrencyCurrentDateController(MongoClientBase MClient)
        {
            _MClient = MClient;
            _currentDB = (MongoDatabaseBase)_MClient.GetDatabase("Current_Date_Currency");
            _collection = (MongoCollectionBase<CurrentDateCurrencyModel>)_currentDB.GetCollection<CurrentDateCurrencyModel>(_collectinName);

        }

        [HttpGet]
        public async Task<List<CurrentDateCurrencyModel>> Get()
        {
            _collection.DeleteMany(item => item.Id >= 0);
            List<CurrentDateCurrencyModel> Data = new List<CurrentDateCurrencyModel>();

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(_url))
            {
                if (response.IsSuccessStatusCode)
                {
                    dynamic responseData = response.Content.ReadAsStringAsync().Result;
                    Data = JsonConvert.DeserializeObject<List<CurrentDateCurrencyModel>>(responseData);
                    var dataFetchResult = Data.ToList<CurrentDateCurrencyModel>();
                    _collection.InsertMany(dataFetchResult);

                    var collectionData = _collection.Find(item => item.Id >= 0).ToList();

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
            List<CurrentDateCurrencyModel> collectionData = _collection.Find(item => item.Id >= 0).ToList();

            switch (key)
            {
                case CurrentDateCurrencyEnum.Text:
                    collectionData.Sort((x, y) => {
                        return (order == SortOrder.ascending)
                        ? String.Compare(x.Text, y.Text, StringComparison.CurrentCulture)
                        : String.Compare(y.Text, x.Text, StringComparison.CurrentCulture);
                    });
                    break;

                case CurrentDateCurrencyEnum.Rate:
                    if (order == SortOrder.ascending)
                    {

                        collectionData = collectionData.OrderBy(collectinItem => collectinItem.Rate).ToList();
                    }
                    else 
                    {
                        collectionData = collectionData.OrderByDescending(collectinItem => collectinItem.Rate).ToList();
                    }
                    break;

                case CurrentDateCurrencyEnum.Currency:
                    collectionData.Sort((x, y) => {
                        return (order == SortOrder.ascending)
                        ? String.Compare(x.Currency, y.Currency, StringComparison.CurrentCulture)
                        : String.Compare(y.Currency, x.Currency, StringComparison.CurrentCulture);
                    });
                    break;

                case CurrentDateCurrencyEnum.ExchangeDate:
                    collectionData.Sort((x, y) => {
                        return (order == SortOrder.ascending)
                        ? DateTime.Compare(DateTime.Parse(x.ExchangeDate), DateTime.Parse(y.ExchangeDate))
                        : DateTime.Compare(DateTime.Parse(y.ExchangeDate), DateTime.Parse(x.ExchangeDate));
                    });
                    break;
            }

            return collectionData;
        }
    }
}
