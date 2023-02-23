using System.Globalization;
using Newtonsoft.Json;

namespace LoadDataLibrary.Models
{
    public class CurrentDateCurrencyModel
    {
        private string _exchangeDate = "";

        private readonly string _format = "dd.MM.yyyy";

        [JsonProperty("r030")]
        public int Id { get; set; }

        [JsonProperty("txt")]
        public string Text { get; set; } = "";

        [JsonProperty("rate")]
        public double Rate { get; set; }

        [JsonProperty("cc")]
        public string Currency { get; set; } = "";

        [JsonProperty("exchangedate")]
        public string ExchangeDate
        {
            get => _exchangeDate;
            set => _exchangeDate = value;
        }
        
        public DateTime ExchangeDateObject
        {
            get => DateTime.Parse(_exchangeDate);
            set => _exchangeDate = value.ToString(_format, CultureInfo.CurrentCulture);
        }
    }
}

