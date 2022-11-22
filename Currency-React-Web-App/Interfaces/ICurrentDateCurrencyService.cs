using Currency_React_Web_App.Enums;
using LoadDataLibrary.Models;

namespace Currency_React_Web_App.Interfaces;

public interface ICurrentDateCurrencyService
{
    public List<CurrentDateCurrencyModel> SortByCurrencyFieldName
    (
        List<CurrentDateCurrencyModel> data, 
        SortOrder order,
        SortByFieldEnum key
        );
}