using Microsoft.AspNetCore.Mvc;
using Currency_React_Web_App.Enums;
using Currency_React_Web_App.Interfaces;
using Currency_React_Web_App.Services;
using LoadDataLibrary;
using LoadDataLibrary.Interfaces;
using LoadDataLibrary.Models;

namespace Currency_React_Web_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyCurrentDateController : ControllerBase
    {
        private readonly ICurrentDateCurrencyCache _currencyCache;
        
        private readonly SortDateCurrencyService _currencyService = new ();
        private readonly ILoadDataService _fetchDataService;
        private readonly string _cacheKey;

        public CurrencyCurrentDateController(IMongoService mongoService , ICurrentDateCurrencyCache currencyCache)
        {
            _currencyCache = currencyCache;
            _fetchDataService = new LoadDataService(mongoService);
            _cacheKey = GetType().Name;
        }
        
        [HttpGet]
        public ActionResult<List<CurrentDateCurrencyModel>> Get([FromQuery] string searchValue, [FromQuery] SortByFieldEnum key, [FromQuery] SortOrder order)
        {
            List<CurrentDateCurrencyModel> collectionData = GetCachedCurrencyData();

                if (!String.IsNullOrEmpty(searchValue) && searchValue != "null")
                {
                    collectionData = FilterCurrencyData(collectionData, searchValue);
                }
                if (!String.IsNullOrEmpty(key.ToString()) && !String.IsNullOrEmpty(order.ToString()))
                {
                    collectionData = SortCurrencyData(collectionData, key, order);
                }
            return collectionData;
        }

        private List<CurrentDateCurrencyModel> GetCachedCurrencyData()
        {
            return _currencyCache.GetMemoryCache(() => _fetchDataService.GetCurrencyDataFromDb(item=> item.Id >= 0), _cacheKey);
        }

        private List<CurrentDateCurrencyModel> FilterCurrencyData(List<CurrentDateCurrencyModel> data, string searchValue)
        {
            string lowerCaseValue = searchValue.ToLower();
        
            return data.
                Where(item =>
                    item.Text.ToLower().Contains(lowerCaseValue) || item.Currency.ToLower().Contains(lowerCaseValue))
                .Select(item => item)
                .ToList();
        }

        private List<CurrentDateCurrencyModel> SortCurrencyData(List<CurrentDateCurrencyModel> data, SortByFieldEnum key, SortOrder order)
        {
            return _currencyService.SortByCurrencyFieldName(data, order, key);
        }
        
    }
}
