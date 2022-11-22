using LoadDataLibrary;
using LoadDataLibrary.Interfaces;
using LoadDataLibrary.Models;
using MongoDB.Driver.Linq;
using Moq;

namespace CurrencyTests.LoadDataLibrary.Tests;

public class LoadDataServiceTest
{
    private Mock<LoadDataService> _sut;
    private readonly List<CurrentDateCurrencyModel> _mockData;
    private readonly Mock<IMongoService> _mongoServiceMock;
    private List<CurrentDateCurrencyModel> _filteredData;
    
    
    private Mock<IMongoQueryable<CurrentDateCurrencyModel>> _mongoQueryableMock;

    public LoadDataServiceTest()
    {
        _sut = new Mock<LoadDataService>();
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
            new CurrentDateCurrencyModel()
            {
                Id = -36,
                Text = "Австралійський долар",
                Rate = 22.3311,
                Currency = "NID",
                ExchangeDate = "2022.11.19"
            }
        };
        _filteredData = new List<CurrentDateCurrencyModel>();
        _mongoServiceMock = new Mock<IMongoService>();
        
        _mongoQueryableMock = new Mock<IMongoQueryable<CurrentDateCurrencyModel>>();

    }

    private void DataInitializer()
    {
        var queryableList = _mockData.AsQueryable();

        _mongoQueryableMock = new Mock<IMongoQueryable<CurrentDateCurrencyModel>>();
        
        _mongoQueryableMock
            .As<IQueryable<CurrentDateCurrencyModel>>()
            .Setup(x => x.Provider)
            .Returns(queryableList.Provider);
        _mongoQueryableMock
            .As<IQueryable<CurrentDateCurrencyModel>>()
            .Setup(x => x.Expression)
            .Returns(queryableList.Expression);
        _mongoQueryableMock
            .As<IQueryable<CurrentDateCurrencyModel>>()
            .Setup(x => x.ElementType)
            .Returns(queryableList.ElementType);
        _mongoQueryableMock
            .As<IQueryable<CurrentDateCurrencyModel>>()
            .Setup(x => x.GetEnumerator())
            .Returns(() => queryableList.GetEnumerator());
        
        _filteredData = _mockData
            .Where((item) => item?.Id >= 0)
            .Select(item=> item)
            .ToList();
        
        _mongoServiceMock
            .Setup(x => x.GetDataFromCollection<CurrentDateCurrencyModel>(
                It.IsAny<string>(), It.IsAny<string>()
                )).Returns(_mongoQueryableMock.Object);
        
        _sut = new Mock<LoadDataService>(_mongoServiceMock.Object);
    }

    [Fact]
    public void GetCurrencyDataFromDb_ShouldReturnListOfCurrency()
    {
        // Arrange
        DataInitializer();
        // Act
        var result = _sut.Object.GetCurrencyDataFromDb(item => item.Id >= 0);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(_filteredData, result);
    } 
    
    [Fact]
    public void GetCurrencyDataFromDb_ShouldReturnListOfCurrencyWithNotNegativeId()
    {
        // Arrange
        DataInitializer();
        
        // Act
        var result = _sut.Object.GetCurrencyDataFromDb(item => item.Id >= 0);
        
        // Assert
        Assert.Contains(result, item => item.Id >= 0);
    }
}