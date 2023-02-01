using LoadDataLibrary;
using LoadDataLibrary.Interfaces;
using LoadDataLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace Currency_React_Web_App.Controllers;

[ApiController]
[Route("[controller]")]
public class SingleCurrencyController : ControllerBase
{
    private readonly ILoadDataService _fetchDataService;

    public SingleCurrencyController(IMongoService mongoService )
    {
        // _currencyCache = currencyCache;
        _fetchDataService = new LoadDataService(mongoService);
        // _cacheKey = GetType().Name;
    }
    
    [HttpGet]
    public ActionResult<List<OneCurrencyByDates>> Get([FromQuery] string startDate, [FromQuery] string endDate, [FromQuery] string currencyCode)
    {
        return _fetchDataService.GetSingleCurrencyByDates(startDate, endDate, currencyCode).Result;
    }
}