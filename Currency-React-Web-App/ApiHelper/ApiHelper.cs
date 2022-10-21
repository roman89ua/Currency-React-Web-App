using System;
using System.Net.Http.Headers;

namespace Currency_React_Web_App.ApiHelper
{
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; } = new HttpClient();

        public static void InitializeClient()
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder();

            string baseRequestUrl = builder.Configuration.GetValue<string>("Urls:BaseRequestUrl");

            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri(baseRequestUrl);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}

