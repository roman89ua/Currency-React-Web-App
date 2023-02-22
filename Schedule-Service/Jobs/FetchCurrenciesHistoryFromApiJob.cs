using LoadDataLibrary.Interfaces;
using LoadDataLibrary.Models;
using Quartz;

namespace Schedule_Service.Jobs;

public class FetchCurrenciesHistoryFromApiJob : IJob
{
    
    private readonly ILogger<FetchCurrenciesHistoryFromApiJob> _logger;
        
    private readonly ILoadDataService _fetchDataService;


    public FetchCurrenciesHistoryFromApiJob(ILogger<FetchCurrenciesHistoryFromApiJob> logger, ILoadDataService loadDataService)
    {
        _logger = logger; 
        _fetchDataService = loadDataService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _fetchDataService.LoadCurrenciesHistory(_fetchDataService.GetCurrencyDataFromDb<CurrentDateCurrencyModel>(currency => currency.Id >= 0));
        _logger.LogInformation($"FetchCurrenciesHistoryFromApiJob Finished at {DateTime.Now}.");
    }
}