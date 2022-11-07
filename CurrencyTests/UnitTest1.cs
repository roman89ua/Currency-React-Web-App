using Currency_React_Web_App.Services;
using MongoDB.Driver;

namespace CurrencyTests;

public class UnitTest1 : IClassFixture<FetchDataService>
{
    private readonly MongoClientBase _mongoClient;
    
    
    public UnitTest1()
    {
        _mongoClient = new MongoClient("mongodb+srv://mongo1111:mongo1111@claster1.mayzmow.mongodb.net/?retryWrites=true&w=majority");
    }
    
    [Fact]
    public void CurrentDateCurrencyCollectionNotNull()
    {
        IFetchDataService _fetchDataService = new FetchDataService(_mongoClient);
        var getCollectionService = _fetchDataService.GetCurrentDateCurrencyCollection();
        Assert.NotNull(getCollectionService);
    }
}
