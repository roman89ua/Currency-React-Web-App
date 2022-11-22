using Microsoft.AspNetCore.Mvc;
using Currency_React_Web_App.Enums;
using Currency_React_Web_App.Services;
using MongoDbServiceLibrary;
using MongoDbServiceLibrary.Models;

namespace Currency_React_Web_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyCurrentDateController : ControllerBase
    {
        private readonly ICurrentDateCurrencyCache _currencyCache;
        
        private readonly SortDateCurrencyService _currencyService = new ();
        private readonly IMongoDbService _fetchDataService;
        private readonly string _cacheKey;

        public CurrencyCurrentDateController(IMongoOnlyService mongoOnlyService , ICurrentDateCurrencyCache currencyCache)
        {
            _currencyCache = currencyCache;
            _fetchDataService = new MongoDbService(mongoOnlyService);
            _cacheKey = GetType().Name;
        }

        [HttpGet]
        public List<CurrentDateCurrencyModel> Get()
        {
            return _currencyCache.GetMemoryCache(_fetchDataService.GetCurrencyDataFromDb, _cacheKey);
        }

        [HttpGet]
        [Route("sortcurrencydata/{key}/{order}")]
        public ActionResult<List<CurrentDateCurrencyModel>> SortCurrencyData(SortByFieldEnum key, SortOrder order)
        {
            return  _currencyService.SortByCurrencyFieldName(_currencyCache.GetMemoryCache(_fetchDataService.GetCurrencyDataFromDb, _cacheKey), order, key);
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
