using Currency_React_Web_App.Enums;
using Currency_React_Web_App.Services;
using MongoDbServiceLibrary.Models;

namespace CurrencyTests.Currency_React_Web_App.Tests.Services.Test;

public class CurrentDateCurrencyServiceTest
{
    private readonly SortDateCurrencyService _sortService;

    private static readonly List<CurrentDateCurrencyModel> MockData = new ()
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
    public  CurrentDateCurrencyServiceTest ()
    {
        _sortService = new SortDateCurrencyService();
    }
     
    [Fact]
    public void SortByCurrencyFieldName_ShouldNotBeNull()
    {
        List<CurrentDateCurrencyModel> result =
            _sortService.SortByCurrencyFieldName(MockData, SortOrder.Ascending, SortByFieldEnum.Currency);
        Assert.NotNull(result);
    }
    
    [Fact]
    public void SortByCurrencyFieldName_ShouldHaveCorrectLength()
    {
        List<CurrentDateCurrencyModel> result =
            _sortService.SortByCurrencyFieldName(MockData, SortOrder.Ascending, SortByFieldEnum.Currency);
        Assert.True(result.Count == MockData.Count);
    }
    
    [Theory]
    [InlineData( SortByFieldEnum.Currency )]
    [InlineData( SortByFieldEnum.Rate )]
    [InlineData( SortByFieldEnum.Text )]
    [InlineData( SortByFieldEnum.ExchangeDate )]
    public void SortByCurrencyFieldName_ShouldSortBySelectedFieldAscending(SortByFieldEnum field)
    {
        List<CurrentDateCurrencyModel> result =
            _sortService.SortByCurrencyFieldName(MockData, SortOrder.Ascending, field);
        var ascending = true;
        
        for (int i = 0; i < result.Count - 1; i++)
        {
            bool condition = true;
            
            if (field == SortByFieldEnum.Currency)
                condition = StringComparer.Ordinal.Compare(result[i].Currency, result[i + 1].Currency) > 0;
            
            if (field == SortByFieldEnum.Text)
                condition = StringComparer.Ordinal.Compare(result[i].Text, result[i + 1].Text) > 0;
            
            if (field == SortByFieldEnum.Rate) condition = result[i].Rate > result[i + 1].Rate;
            
            if(field == SortByFieldEnum.ExchangeDate) 
                condition = DateTime.Parse(result[i].ExchangeDate) > DateTime.Parse(result[i + 1].ExchangeDate);
            
            
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
    [InlineData( SortByFieldEnum.ExchangeDate )]
    public void SortByCurrencyFieldName_ShouldSortBySelectedFieldDescending(SortByFieldEnum field)
    {
        List<CurrentDateCurrencyModel> result =
            _sortService.SortByCurrencyFieldName(MockData, SortOrder.Descending, field);
        var descending = true;
        
        for (int i = 0; i < result.Count - 1; i++)
        {
            bool condition = true;
            
            if (field == SortByFieldEnum.Currency)
                condition = StringComparer.Ordinal.Compare(result[i].Currency, result[i + 1].Currency) < 0;
            
            if (field == SortByFieldEnum.Text)
                condition = StringComparer.Ordinal.Compare(result[i].Text, result[i + 1].Text) < 0;
            
            if (field == SortByFieldEnum.Rate) condition = result[i].Rate < result[i + 1].Rate;
            
            if(field == SortByFieldEnum.ExchangeDate) 
                condition = DateTime.Parse(result[i].ExchangeDate) < DateTime.Parse(result[i + 1].ExchangeDate);
    
            if (condition)
            {
                descending = false;
                break;
            }
        }
            
        Assert.True(descending);
    }
    public static IEnumerable<object[]> TestData()
    {
        /*
         initial value,
         expect value, 
         field name to sort by, 
         sort order
         */
        yield return new object[]
        {
            MockData,
            MockData.OrderByDescending(collectionItem => collectionItem.Text).ToList(),
            SortByFieldEnum.Text,
            SortOrder.Descending
        };
        yield return new object[]
        {
            MockData,
            MockData.OrderByDescending(collectionItem => collectionItem.Currency).ToList(),
            SortByFieldEnum.Currency,
            SortOrder.Descending
        };
        yield return new object[]
        {
            MockData,
            MockData.OrderByDescending(collectionItem => collectionItem.Rate).ToList(),
            SortByFieldEnum.Rate,
            SortOrder.Descending
        };
        yield return new object[]
        {
            MockData,
            MockData.OrderByDescending(collectionItem => DateTime.Parse(collectionItem.ExchangeDate)).ToList(),
            SortByFieldEnum.ExchangeDate,
            SortOrder.Descending
        };
        yield return new object[]
        {
            MockData,
            MockData.OrderBy(collectionItem => collectionItem.Text).ToList(),
            SortByFieldEnum.Text,
            SortOrder.Ascending
        };
        yield return new object[]
        {
            MockData,
            MockData.OrderBy(collectionItem => collectionItem.Currency).ToList(),
            SortByFieldEnum.Currency,
            SortOrder.Ascending
        };
        yield return new object[]
        {
            MockData,
            MockData.OrderBy(collectionItem => collectionItem.Rate).ToList(),
            SortByFieldEnum.Rate,
            SortOrder.Ascending
        };
        yield return new object[]
        {
            MockData,
            MockData.OrderBy(collectionItem => DateTime.Parse(collectionItem.ExchangeDate)).ToList(),
            SortByFieldEnum.ExchangeDate,
            SortOrder.Ascending
        };
    }
    
    [Theory]
    [MemberData(nameof(TestData))]
    public void SortByCurrencyFieldName_ShouldSortByAllFields(
        List<CurrentDateCurrencyModel> input,
        List<CurrentDateCurrencyModel> expect,  
        SortByFieldEnum field,
        SortOrder sortOrder
        )
    {
        List<CurrentDateCurrencyModel> result = _sortService.SortByCurrencyFieldName(input, sortOrder, field);

        Assert.Equal(expect, result);
    }
}
