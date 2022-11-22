using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MongoDbServiceLibrary;

public class MongoOnlyService : IMongoOnlyService
{
    private readonly MongoClientBase _mongoClient;
    private readonly ILogger<MongoOnlyService> _logger;

    public MongoOnlyService(MongoClientBase mongoClient, ILogger<MongoOnlyService> logger)
    {
        _logger = logger;
        _mongoClient = mongoClient;
    }

    private IMongoDatabase GetDb(string dbName)
    {
        return _mongoClient.GetDatabase(dbName);
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

}

public interface IMongoOnlyService
{
    public List<T> GetDataFromCollection<T>(string dbName, string collectionName, Expression<Func<T, bool>> predicate);
    public Task  ClearDbCollection<T>(string dbName, string collectionName);
    public Task  RefillCollection<T>(string dbName, string collectionName, IEnumerable<T> newData);

}