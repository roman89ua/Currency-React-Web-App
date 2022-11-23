using System.Linq.Expressions;
using MongoDB.Driver.Linq;

namespace LoadDataLibrary.Interfaces;

public interface IMongoService
{ 
        public IMongoQueryable<T> GetDataFromCollection<T>(string dbName, string collectionName);
        public Task  ClearDbCollection<T>(string dbName, string collectionName);
        public Task  RefillCollection<T>(string dbName, string collectionName, IEnumerable<T> newData);
}