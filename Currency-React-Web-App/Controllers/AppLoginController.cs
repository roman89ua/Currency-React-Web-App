using LoadDataLibrary.Interfaces;
using LoadDataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Linq;

namespace Currency_React_Web_App.Controllers;
[ApiController]
[Route("[controller]")]
public class AppLoginController : Controller
{
    private readonly ILoginService _loginService;
    
    public AppLoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpGet]
    public  async Task<List<UserModel>> Get()
    {
        IMongoQueryable<UserModel> queryableList = await _loginService.GetQueryableUsersList();
        
        var listOfUsers = queryableList.Where(user => user.Name == "Roman").Select(user => user).ToList();
        return listOfUsers;
    }
}