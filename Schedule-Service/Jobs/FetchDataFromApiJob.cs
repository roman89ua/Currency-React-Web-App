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
        
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("@@@@@ FetchDataFromApiJob WORKS!!! {Now}", DateTime.Now);
            _fetchDataService.DataBaseRefresh();
            return Task.CompletedTask;
        }
    }
}