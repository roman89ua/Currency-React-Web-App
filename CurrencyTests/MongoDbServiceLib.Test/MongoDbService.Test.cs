using MongoDbServiceLibrary;
using MongoDbServiceLibrary.Models;
using Moq;

namespace CurrencyTests.MongoDbServiceLib.Test;

public class MongoDbServiceTest
{
    private Mock<MongoDbService> _sut;
    private List<CurrentDateCurrencyModel> _mockData;
    private Mock<IMongoOnlyService> _mongoServiceOnlyMock;

    public MongoDbServiceTest()
    {
        _sut = new Mock<MongoDbService>();
        _mockData = new List<CurrentDateCurrencyModel>();
        _mongoServiceOnlyMock = new Mock<IMongoOnlyService>();
    }

    [Fact]
    public void GetCurrencyDataFromDb_ShouldReturnListOfCurrency()
    {
        // Arrange
        _mockData = new List<CurrentDateCurrencyModel>()
        {
            new CurrentDateCurrencyModel()
            {
                Id = 36,
                Text = "Австралійський долар",
                Rate = 23.6654,
                Currency = "AUD",
                ExchangeDate = "2022.11.11"
            },
            new CurrentDateCurrencyModel()
            {
                Id = 124,
                Text = "Канадський долар",
                Rate = 27.2068,
                Currency = "CAD",
                ExchangeDate = "2022.11.10"
            },
            new CurrentDateCurrencyModel()
            {
                Id = 208,
                Text = "Чеська крона",
                Rate = 1.5106,
                Currency = "CZK",
                ExchangeDate = "2022.11.9"
            },
            new CurrentDateCurrencyModel()
            {
                Id = 376,
                Text = "Новий ізраїдьський шекель",
                Rate = 10.3112,
                Currency = "ILS",
                ExchangeDate = "2022.11.4"
            },
        };
        _mongoServiceOnlyMock = new Mock<IMongoOnlyService>();
        
        _mongoServiceOnlyMock
            .Setup(x => x.GetDataFromCollection<CurrentDateCurrencyModel>(
            It.IsAny<string>(), It.IsAny<string>(), item => item.Id >= 0
            )).Returns(_mockData);
        _sut = new Mock<MongoDbService>(_mongoServiceOnlyMock.Object);
       
        // Act
        var result = _sut.Object.GetCurrencyDataFromDb();
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(_mockData, result);
    }
}