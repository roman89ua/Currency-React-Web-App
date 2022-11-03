using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Currency_React_Web_App.Enums;
using Currency_React_Web_App.Services;

namespace Currency_React_Web_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyCurrentDateController : ControllerBase
    {
        private readonly ICurrentDateCurrencyCache _currencyCache;
        
        private readonly CurrentDateCurrencyService _currencyService = new ();
        private readonly FetchDataService _fetchDataService;
        private readonly string _cacheKey;

        public CurrencyCurrentDateController(MongoClientBase mongoClient, ICurrentDateCurrencyCache currencyCache)
        {
            _currencyCache = currencyCache;
            _fetchDataService = new FetchDataService(mongoClient);
            _cacheKey = GetType().Name;
        }

        [HttpGet]
        public List<CurrentDateCurrencyModel> Get()
        {
            return _currencyCache.GetMemoryCache(_fetchDataService.GetCurrencyDataFromDb, _cacheKey);
        }

        [HttpGet]
        [Route("sortcurrencydata/{key}/{order}")]
        public ActionResult<List<CurrentDateCurrencyModel>> SortCurrencyData(CurrentDateCurrencyEnum key, SortOrder order)
        {
            return _currencyService.SortByCurrencyFieldName(_currencyCache.GetMemoryCache(_fetchDataService.GetCurrencyDataFromDb, _cacheKey), order, key);
        }

        [HttpGet]
        [Route("filtercurrencydata/{value}")]
        public ActionResult<List<CurrentDateCurrencyModel>> FilterCurrencyData(string value)
        {
            List<CurrentDateCurrencyModel> collectionData = _currencyCache.GetMemoryCache(_fetchDataService.GetCurrencyDataFromDb, _cacheKey);

            if (String.IsNullOrEmpty(value) || value == "null") return collectionData;

            string lowerCaseValue = value.ToLower();

            List<CurrentDateCurrencyModel> sortedData = collectionData.
                Where(item =>
                    item.Text.ToLower().Contains(lowerCaseValue) || item.Currency.ToLower().Contains(lowerCaseValue))
                .Select(item => item)
                .ToList();

            return sortedData;
        }
    }
}
