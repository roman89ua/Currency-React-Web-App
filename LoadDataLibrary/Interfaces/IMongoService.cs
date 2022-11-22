using System.Linq.Expressions;

namespace LoadDataLibrary.Interfaces;

public interface IMongoService
{ 
        public List<T> GetDataFromCollection<T>(string dbName, string collectionName, Expression<Func<T, bool>> predicate);
        public Task  ClearDbCollection<T>(string dbName, string collectionName);
        public Task  RefillCollection<T>(string dbName, string collectionName, IEnumerable<T> newData);
}