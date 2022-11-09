using MongoDbServiceLibrary;
using Quartz;

namespace Schedule_Service.Jobs
{
    [DisallowConcurrentExecution]
    public class FetchDataFromApiJob : IJob
    {
        private readonly ILogger<FetchDataFromApiJob> _logger;
        
        private readonly IMongoDbService _fetchDataService;


        public FetchDataFromApiJob(ILogger<FetchDataFromApiJob> logger, IMongoDbService mongoDbService)
        {
            _logger = logger; 
            _fetchDataService = mongoDbService;
        }
        
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("@@@@@ FetchDataFromApiJob WORKS!!! {Now}", DateTime.Now);
            _fetchDataService.DataBaseRefresh();
            return Task.CompletedTask;
        }
    }
}