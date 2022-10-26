using System;
using System.Net.Http.Headers;

namespace Currency_React_Web_App
{
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; } = new HttpClient();

        public ApiHelper(string baseRequestUrl)
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri(baseRequestUrl);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}

