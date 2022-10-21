using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Currency_React_Web_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyCurrentDateController : ControllerBase
    {
        private string _url = "NBUStatService/v1/statdirectory/exchangenew?json";


        [HttpGet]
        public async Task<List<CurrentDateCurrencyModel>> Get()
        {
            ApiHelper.ApiHelper.InitializeClient();

            List<CurrentDateCurrencyModel> Data = new List<CurrentDateCurrencyModel>();

            using (HttpResponseMessage response = await ApiHelper.ApiHelper.ApiClient.GetAsync(_url))
            {
                if (response.IsSuccessStatusCode)
                {

                    dynamic currentDateCurrency = response.Content.ReadAsStringAsync().Result;

                    Data = JsonConvert.DeserializeObject<List<CurrentDateCurrencyModel>>(currentDateCurrency);

                    var result = Data.ToList<CurrentDateCurrencyModel>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
