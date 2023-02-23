using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace LoadDataLibrary.Interfaces;

public interface IMongoService
{
        public List<string> GetDbCollectionsNameList(string dbName);
        public Task CreateDbCollection(string dbName, string newCollectionName);
        public Task AddOneToCollection<T>(string dbName, string collectionName, T document);
        public Task AddManyToCollection<T>(string dbName, string collectionName, List<T> documents);
        public IMongoQueryable<T> GetQueryableDataFromCollection<T>(string dbName, string collectionName);
        public Task  ClearDbCollection<T>(string dbName, string collectionName);
        public Task  RefillCollection<T>(string dbName, string collectionName, IEnumerable<T> newData);
}