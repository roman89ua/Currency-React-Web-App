using System.Linq.Expressions;
using LoadDataLibrary;
using LoadDataLibrary.Interfaces;
using LoadDataLibrary.Models;
using Moq;

namespace CurrencyTests.LoadDataLibrary.Tests;

public class LoadDataServiceTest
{
    private Mock<LoadDataService> _sut;
    private List<CurrentDateCurrencyModel> _mockData;
    private Mock<IMongoService> _mongoServiceMock;
    private List<CurrentDateCurrencyModel> _filteredData;

    public LoadDataServiceTest()
    {
        _sut = new Mock<LoadDataService>();
        _mockData = new List<CurrentDateCurrencyModel>();
        _filteredData = new List<CurrentDateCurrencyModel>();
        _mongoServiceMock = new Mock<IMongoService>();
    }

    private void DataInitializer()
    {
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
        _filteredData = _mockData
            .Where((item) => item.Id >= 0)
            .Select(item=> item)
            .ToList();
        
        _mongoServiceMock = new Mock<IMongoService>();
        _mongoServiceMock
            .Setup(x => x.GetDataFromCollection<CurrentDateCurrencyModel>(
                It.IsAny<string>(), It.IsAny<string>(), (item) => item.Id >= 0
            )).Returns(_filteredData);
        
        _sut = new Mock<LoadDataService>(_mongoServiceMock.Object);
    }

    [Fact]
    public void GetCurrencyDataFromDb_ShouldReturnListOfCurrency()
    {
        // Arrange
        DataInitializer();
        // Act
        var result = _sut.Object.GetCurrencyDataFromDb();
        
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
        var result = _sut.Object.GetCurrencyDataFromDb();
        
        // Assert
        Assert.Contains(result, item => item.Id >= 0);
    }
}