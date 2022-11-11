using Currency_React_Web_App.Enums;
using MongoDbServiceLibrary.Models;

namespace Currency_React_Web_App.Services
{
    public class SortDateCurrencyService : ICurrentDateCurrencyService
    {
        public List<CurrentDateCurrencyModel> SortByCurrencyFieldName (List<CurrentDateCurrencyModel> data, SortOrder order, SortByFieldEnum key )
        {
            switch (key)
            {
                case SortByFieldEnum.Text:
                    data.Sort((x, y) => (order == SortOrder.Ascending)
                        ? String.Compare(x.Text, y.Text, StringComparison.CurrentCulture)
                        : String.Compare(y.Text, x.Text, StringComparison.CurrentCulture));
                    break;

                case SortByFieldEnum.Rate:
                    data = order == SortOrder.Ascending 
                        ? data.OrderBy(collectionItem => collectionItem.Rate).ToList() 
                        : data.OrderByDescending(collectionItem => collectionItem.Rate).ToList();
                    break;

                case SortByFieldEnum.Currency:
                    data.Sort((x, y) => (order == SortOrder.Ascending)
                        ? String.Compare(x.Currency, y.Currency, StringComparison.CurrentCulture)
                        : String.Compare(y.Currency, x.Currency, StringComparison.CurrentCulture)
                    );
                    break;

                case SortByFieldEnum.ExchangeDate:
                    data.Sort((x, y) => (order == SortOrder.Ascending)
                        ? DateTime.Compare(DateTime.Parse(x.ExchangeDate), DateTime.Parse(y.ExchangeDate))
                        : DateTime.Compare(DateTime.Parse(y.ExchangeDate), DateTime.Parse(x.ExchangeDate))
                    );
                    break;
            }

            return data;
        }
    }
    
    public interface ICurrentDateCurrencyService
    {
        public List<CurrentDateCurrencyModel> SortByCurrencyFieldName(List<CurrentDateCurrencyModel> data, SortOrder order,
            SortByFieldEnum key);
    }
}

