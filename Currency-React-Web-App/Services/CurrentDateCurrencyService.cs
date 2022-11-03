using Currency_React_Web_App.Enums;

namespace Currency_React_Web_App.Services
{
    public class CurrentDateCurrencyService : ICurrentDateCurrencyService
    {
        public List<CurrentDateCurrencyModel> SortByCurrencyFieldName (List<CurrentDateCurrencyModel> data, SortOrder order, CurrentDateCurrencyEnum key )
        {
            switch (key)
            {
                case CurrentDateCurrencyEnum.Text:
                    data.Sort((x, y) => (order == SortOrder.Ascending)
                        ? String.Compare(x.Text, y.Text, StringComparison.CurrentCulture)
                        : String.Compare(y.Text, x.Text, StringComparison.CurrentCulture));
                    break;

                case CurrentDateCurrencyEnum.Rate:
                    data = order == SortOrder.Ascending ? data.OrderBy(collectionItem => collectionItem.Rate).ToList() : data.OrderByDescending(collectionItem => collectionItem.Rate).ToList();
                    break;

                case CurrentDateCurrencyEnum.Currency:
                    data.Sort((x, y) => (order == SortOrder.Ascending)
                        ? String.Compare(x.Currency, y.Currency, StringComparison.CurrentCulture)
                        : String.Compare(y.Currency, x.Currency, StringComparison.CurrentCulture)
                    );
                    break;

                case CurrentDateCurrencyEnum.ExchangeDate:
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
            CurrentDateCurrencyEnum key);
    }
}

