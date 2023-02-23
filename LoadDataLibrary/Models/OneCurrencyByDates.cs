using System.Globalization;
using System.Text.Json;
using LoadDataLibrary.Helpers;
using LoadDataLibrary.Interfaces;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace LoadDataLibrary.Models;

public class OneCurrencyByDates
{
    private string _exchangeDate = "";

    private string _calculationDate = "";

    private readonly IDateValidator _dateValidator = new DateValidator();

    private readonly string[] _formats =
    {
        "d/M/yyyy", "dd/MM/yyyy",
        "M/d/yyyy", "MM/dd/yyyy",
        "yyyy/MM/dd", "yyyy/M/d",
        "yyyy/dd/MM", "yyyy/d/M",
        "d.M.yyyy", "dd.MM.yyyy",
        "M.d.yyyy", "MM.dd.yyyy",
        "yyyy.MM.dd", "yyyy.M.d",
        "yyyy.dd.MM", "yyyy.d.M",
        "d-M-yyyy", "dd-MM-yyyy",
        "M-d-yyyy", "MM-dd-yyyy",
        "yyyy-MM-dd", "yyyy-M-d",
        "yyyy-dd-MM", "yyyy-d-M"
    };

    public ObjectId Id { get; set; }

    
    [JsonProperty("exchangedate")]
    public string ExchangeDate
    {
        get => _exchangeDate;
        set => _exchangeDate = value;
        
    }
    public DateTime ExchangeDateObject
    {
        get =>  DateTime.Parse(ExchangeDate);
        set
        {
            _exchangeDate = value.ToString(_formats[9]);
            ExchangeDate = _exchangeDate;
        }
    }

    [JsonProperty("r030")]
    public int CurrencyId { get; set; }

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
            if (_dateValidator.IsValidDate(value.Trim(), _formats))
            {
                DateTime dDate = DateTime.Parse(value);
                _calculationDate = dDate.ToString(_formats[9]);
            }
            else
            {
                _calculationDate = value.Trim();
            }

        }
    }
}
    
