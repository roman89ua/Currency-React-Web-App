using Microsoft.AspNetCore.Mvc;
using LoadDataLibrary;
using LoadDataLibrary.Interfaces;

namespace Currency_React_Web_App.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : Controller
{
    private readonly ILoadDataService _fetchDataService;
    public HomeController(ILoadDataService loadDataService)
    {
        _fetchDataService = loadDataService;
    }

    [HttpGet]
    [Route("updateDbOnAppStart")]
    public async Task UpdateDbOnAppStart()
    {
        await _fetchDataService.DataBaseRefresh();
    }
}