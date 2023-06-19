using LoadDataLibrary.Models;
using MongoDB.Driver.Linq;

namespace LoadDataLibrary.Interfaces;

public interface ILoginService
{
    public Task<IMongoQueryable<UserModel>> GetQueryableUsersList();

}