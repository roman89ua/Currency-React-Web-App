﻿using System;
using System.Net.Http.Headers;
using Currency_React_Web_App.Services;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Currency_React_Web_App.Clients
{
    public class CurrencyClient : ICurrencyClient
    {
        private static HttpClient? _httpClient;

        private static FetchDataService? _dbService;

        public CurrencyClient(HttpClient? httpClient, MongoClientBase mongoClient)
        {
            _httpClient = httpClient;
            _dbService = new FetchDataService(mongoClient);
        }

        public async Task DataBaseRefresh()
        {
            HttpResponseMessage response =
                await _httpClient?.GetAsync("NBUStatService/v1/statdirectory/exchangenew?json")!;
            dynamic responseData = response.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<List<CurrentDateCurrencyModel>>(responseData);
            List<CurrentDateCurrencyModel> dataFetchResult = data.ToList<CurrentDateCurrencyModel>();
            if (dataFetchResult.Count > 0)
            {
                await _dbService!.GetCurrentDateCurrencyCollection().DeleteManyAsync(item => item.Id >= 0);
                await _dbService.GetCurrentDateCurrencyCollection().InsertManyAsync(dataFetchResult);
            }
        }
    }

    public interface ICurrencyClient
    {
        public Task DataBaseRefresh();
    }
}

