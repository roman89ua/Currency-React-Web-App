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
    
    public List<string> GetDbCollectionsNameList(string dbName) =>  GetDb(dbName).ListCollectionNamesAsync().Result.ToList();

    public async Task CreateDbCollection(string dbName, string newCollectionName)
    {
        await GetDb(dbName).CreateCollectionAsync(newCollectionName);
    }

    public async Task AddOneToCollection<T>(string dbName, string collectionName, T document)
    {
         await GetDb(dbName).GetCollection<T>(collectionName).InsertOneAsync(document);
    }
    
    public async Task AddManyToCollection<T>(string dbName, string collectionName, List<T> documents)
    {
        await GetDb(dbName).GetCollection<T>(collectionName).InsertManyAsync(documents);
    }

    public IMongoQueryable<T> GetQueryableDataFromCollection<T>(string dbName, string collectionName)
    {
        return GetDb(dbName)
            .GetCollection<T>(collectionName)
            .AsQueryable();
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
