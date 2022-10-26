using System;
using Newtonsoft.Json;
using System.Globalization;

namespace Currency_React_Web_App
{
    public class CurrentDateCurrencyModel
    {
        private string _ExchangeDate = "";

        public CurrentDateCurrencyModel()
        {
        }

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
            get
            {
                return _ExchangeDate;
            }
            set
            {
                DateTime dDate = DateTime.Parse(value);

                _ExchangeDate = dDate.ToString("yyyy.MM.dd", CultureInfo.InvariantCulture);
            }
        }
    }
}

