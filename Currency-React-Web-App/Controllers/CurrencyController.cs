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
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrentDateCurrencyCache _currencyCache;
        private readonly SortDateCurrencyService _currencyService = new ();
        private readonly ILoadDataService _loadDataService;
        private readonly string _cacheKey;

        public CurrencyController(IMongoService mongoService , ICurrentDateCurrencyCache currencyCache)
        {
            _currencyCache = currencyCache;
            _loadDataService = new LoadDataService(mongoService);
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

        [HttpGet]
        [Route("CurrencyHistoryCreateUpdate")]
        public async Task CurrencyHistoryCreateUpdate()
        {
            await _loadDataService.LoadCurrenciesHistory(GetCachedCurrencyData());
        }
        
        [HttpGet]
        [Route("SingleCurrencyByDates")]
        public List<OneCurrencyByDatesModel> SingleCurrencyByDates(
            [FromQuery] string startDate,
            [FromQuery] string endDate,
            [FromQuery] string currencyCode)
        {
            
            DateTime startD = DateTime.Parse(startDate);
            DateTime endD = DateTime.Parse(endDate);
             
            return _currencyCache
                .GetMemoryCache(() => 
                        _loadDataService
                            .GetSingleCurrencyDataHistoryFromDb<OneCurrencyByDatesModel>(
                                currency => currency.ExchangeDateObject > startD && currency.ExchangeDateObject <= endD, currencyCode), 
                    $"{startDate}-${endDate}-{currencyCode}"
                );
        }
            
        private List<CurrentDateCurrencyModel> GetCachedCurrencyData()
        {
            return _currencyCache.GetMemoryCache(() => _loadDataService.GetCurrencyDataFromDb<CurrentDateCurrencyModel>(item=> item.Id >= 0), _cacheKey);
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
