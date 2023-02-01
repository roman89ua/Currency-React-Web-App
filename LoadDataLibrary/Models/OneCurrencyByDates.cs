using System.Globalization;
using Newtonsoft.Json;

namespace LoadDataLibrary.Models;

public class OneCurrencyByDates
{
    private string _exchangeDate = "";
    private string _calculationDate = "";
    
    [JsonProperty("exchangedate")]
    public string ExchangeDate
    {
        get => _exchangeDate;
        set
        {
            DateTime dDate = DateTime.Parse(value);

            _exchangeDate = dDate.ToString("yyyy.MM.dd", CultureInfo.InvariantCulture);
        }
    }

    [JsonProperty("r030")]
    public int Id { get; set; }

    [JsonProperty("cc")]
    public string Currency { get; set; } = "";
    
    [JsonProperty("txt")]
    public string Text { get; set; } = "";
    
    [JsonProperty("enname")]
    public string EnglishName { get; set; } = "";

    [JsonProperty("rate")]
    public double Rate { get; set; }
    
    [JsonProperty("units")]
    public int Units { get; set; }

    [JsonProperty("rate_per_unit")]
    public double RatePerUnit { get; set; }

    [JsonProperty("group")]
    public int Group { get; set; }

    [JsonProperty("calcdate")]
    public string CalculationDate
    {
        get => _calculationDate;
        set
        {
            DateTime dDate = DateTime.Parse(value);

            _calculationDate = dDate.ToString("yyyy.MM.dd", CultureInfo.InvariantCulture);
        }
    }
    
}
