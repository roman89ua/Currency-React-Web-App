using Microsoft.AspNetCore.Mvc;
using MongoDbServiceLibrary;

namespace Currency_React_Web_App.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : Controller
{
    private readonly IMongoDbService _fetchDataService;
    public HomeController(IMongoDbService mongoDbService)
    {
        _fetchDataService = mongoDbService;
    }

    [HttpGet]
    [Route("updatedbonappstart")]
    public async Task UpdateDbOnAppStart()
    {
        await _fetchDataService.DataBaseRefresh();
    }
}