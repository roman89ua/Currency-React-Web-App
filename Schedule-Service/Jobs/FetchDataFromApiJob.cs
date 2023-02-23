using LoadDataLibrary.Interfaces;
using Quartz;

namespace Schedule_Service.Jobs
{
    [DisallowConcurrentExecution]
    public class FetchDataFromApiJob : IJob
    {
        private readonly ILogger<FetchDataFromApiJob> _logger;
        
        private readonly ILoadDataService _fetchDataService;


        public FetchDataFromApiJob(ILogger<FetchDataFromApiJob> logger, ILoadDataService loadDataService)
        {
            _logger = logger; 
            _fetchDataService = loadDataService;
        }
        
        public async Task Execute(IJobExecutionContext context)
        {
            await _fetchDataService.DataBaseRefresh();
            _logger.LogInformation($"FetchCurrenciesHistoryFromApiJob Finished at {DateTime.Now}.");
        }
    }
}