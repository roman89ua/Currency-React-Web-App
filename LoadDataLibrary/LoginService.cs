using LoadDataLibrary.Interfaces;
using LoadDataLibrary.Models;
using MongoDB.Driver.Linq;

namespace LoadDataLibrary;

public class LoginService : ILoginService
{
    private readonly IMongoService _mongoService;

    private static string DbName => "users";
    
    private static string CollectionName => "user";

    public LoginService(IMongoService mongoService)
    {
        _mongoService = mongoService;
    }

    public Task<IMongoQueryable<UserModel>> GetQueryableUsersList()
    {
        return Task.FromResult(_mongoService.GetQueryableDataFromCollection<UserModel>(DbName, CollectionName));
    }
    
    
}