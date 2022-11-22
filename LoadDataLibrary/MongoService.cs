using System.Linq.Expressions;
using LoadDataLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace LoadDataLibrary;

public class MongoService : IMongoService
{
    private readonly MongoClientBase _mongoClient;
    private readonly ILogger<MongoService> _logger;

    public MongoService(MongoClientBase mongoClient, ILogger<MongoService> logger)
    {
        _logger = logger;
        _mongoClient = mongoClient;
    }

    public List<T> GetDataFromCollection<T>(string dbName, string collectionName, Expression<Func<T, bool>> predicate)
    {
        return GetDb(dbName)
            .GetCollection<T>(collectionName)
            .AsQueryable()
            .Where(predicate)
            .Select(item => item)
            .ToList();
    }

    public async Task ClearDbCollection<T>(string dbName, string collectionName)
    {
        try
        {
           await GetDb(dbName).GetCollection<T>(collectionName).DeleteManyAsync(_ => true);
        }
        catch (Exception e)
        {
            _logger.LogInformation("ClearDbCollection method error message: {}", e.Message);
        }
    }
    
    public async Task RefillCollection<T>(string dbName, string collectionName, IEnumerable<T> newData)
    {
        try
        {
            await GetDb(dbName).GetCollection<T>(collectionName).InsertManyAsync(newData);
        }
        catch (Exception e)
        {
            _logger.LogInformation("RefillCollection method error message: {}", e.Message);
        }
    }
    private IMongoDatabase GetDb(string dbName)
    {
        return _mongoClient.GetDatabase(dbName);
    }
}
