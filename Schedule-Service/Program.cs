using MongoDB.Driver;
using LoadDataLibrary;
using LoadDataLibrary.Clients;
using LoadDataLibrary.Interfaces;
using Quartz;
using Schedule_Service.Jobs;
using Schedule_Service.Jobs.Configuration;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        string mongoConnectionString = hostContext.Configuration.GetValue<string>("urls:MongoDBUri");
        string apiDefaultString = hostContext.Configuration.GetValue<string>("urls:BaseRequestUrl");
        
        
        // LoadDataLibrary start
        services.AddSingleton<MongoClientBase, MongoClient>(_ => new MongoClient(mongoConnectionString));
        services.AddSingleton<ILoadDataService, LoadDataService>();
        services.AddSingleton<IMongoService, MongoService>();
        // LoadDataLibrary end
        
        CurrencyClient.Initialize(apiDefaultString);

        //quartz start
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            q.AddJobAndTrigger<FetchDataFromApiJob>(hostContext.Configuration);
            q.AddJobAndTrigger<FetchCurrenciesHistoryFromApiJob>(hostContext.Configuration);
        });
        
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        //quartz end
    })
    .Build();

await host.RunAsync();


