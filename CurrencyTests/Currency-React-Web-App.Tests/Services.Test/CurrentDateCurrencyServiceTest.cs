using Currency_React_Web_App.Enums;
using Currency_React_Web_App.Services;
using MongoDbServiceLibrary.Models;

namespace CurrencyTests.Currency_React_Web_App.Tests.Services.Test;

public class CurrentDateCurrencyServiceTest
{
    private SortDateCurrencyService _sortService;

    private readonly List<CurrentDateCurrencyModel> _mockData = new List<CurrentDateCurrencyModel>()
    {
        new CurrentDateCurrencyModel(){
            Id = 36,
            Text = "Австралійський долар",
            Rate = 23.6654,
            Currency = "AUD",
            ExchangeDate = "2022.11.10"
        },
        new CurrentDateCurrencyModel(){
            Id = 124,
            Text = "Канадський долар",
            Rate = 27.2068,
            Currency = "CAD",
            ExchangeDate ="2022.11.10"
        },
        new CurrentDateCurrencyModel(){
            Id = 208,
            Text = "Чеська крона",
            Rate = 1.5106,
            Currency = "CZK",
            ExchangeDate ="2022.11.10"
        },
        new CurrentDateCurrencyModel(){
            Id = 376,
            Text = "Новий ізраїдьський шекель",
            Rate = 10.3112,
            Currency = "ILS",
            ExchangeDate ="2022.11.10"
        },
    };
    public  CurrentDateCurrencyServiceTest ()
    {
        _sortService = new SortDateCurrencyService();
    }
     
    [Fact]
    public void SortByCurrencyFieldName_ShouldNotBeNull()
    {
        List<CurrentDateCurrencyModel> result =
            _sortService.SortByCurrencyFieldName(_mockData, SortOrder.Ascending, SortByFieldEnum.Currency);
        Assert.NotNull(result);
    }
    [Fact]
    public void SortByCurrencyFieldName_ShouldHaveCorrectLength()
    {
        List<CurrentDateCurrencyModel> result =
            _sortService.SortByCurrencyFieldName(_mockData, SortOrder.Ascending, SortByFieldEnum.Currency);
        Assert.True(result.Count == _mockData.Count);
    }
    
    [Theory]
    [InlineData( SortByFieldEnum.Currency )]
    [InlineData( SortByFieldEnum.Rate )]
    [InlineData( SortByFieldEnum.Text )]
    public void SortByCurrencyFieldName_ShouldSortBySelectedFieldAscending(SortByFieldEnum field)
    {
        List<CurrentDateCurrencyModel> result =
            _sortService.SortByCurrencyFieldName(_mockData, SortOrder.Ascending, field);
        var ascending = true;
        
        for (int i = 0; i < result.Count - 1; i++)
        {
            bool condition = true;
            
            if (field == SortByFieldEnum.Currency)
                condition = StringComparer.Ordinal.Compare(result[i].Currency, result[i + 1].Currency) > 0;
            
            if (field == SortByFieldEnum.Text)
                condition = StringComparer.Ordinal.Compare(result[i].Text, result[i + 1].Text) > 0;
            
            if (field == SortByFieldEnum.Rate) condition = result[i].Rate > result[i + 1].Rate;
            
            
            if (condition)
            {
                ascending = false;
                break;
            }
        }
            
        Assert.True(ascending);
    }
    
    [Theory]
    [InlineData( SortByFieldEnum.Currency )]
    [InlineData( SortByFieldEnum.Rate )]
    [InlineData( SortByFieldEnum.Text )]
    public void SortByCurrencyFieldName_ShouldSortBySelectedFieldDescending(SortByFieldEnum field)
    {
        List<CurrentDateCurrencyModel> result =
            _sortService.SortByCurrencyFieldName(_mockData, SortOrder.Descending, field);
        var descending = true;
        
        for (int i = 0; i < result.Count - 1; i++)
        {
            bool condition = true;
            
            if (field == SortByFieldEnum.Currency)
                condition = StringComparer.Ordinal.Compare(result[i].Currency, result[i + 1].Currency) < 0;
            
            if (field == SortByFieldEnum.Text)
                condition = StringComparer.Ordinal.Compare(result[i].Text, result[i + 1].Text) < 0;
            
            if (field == SortByFieldEnum.Rate) condition = result[i].Rate < result[i + 1].Rate;

            if (condition)
            {
                descending = false;
                break;
            }
        }
            
        Assert.True(descending);
    }
}
