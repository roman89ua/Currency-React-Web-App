using MongoDB.Driver;
using MongoDbServiceLibrary;
using MongoDbServiceLibrary.Clients;
using Quartz;
using Schedule_Service.Jobs;
using Schedule_Service.Jobs.Configuration;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        string mongoConnectionString = hostContext.Configuration.GetValue<string>("urls:MongoDBUri");
        string apiDefaultString = hostContext.Configuration.GetValue<string>("urls:BaseRequestUrl");
        
        
        // MongoDbServiceLibrary start
        services.AddSingleton<MongoClientBase, MongoClient>(_ => new MongoClient(mongoConnectionString));
        services.AddSingleton<IMongoDbService, MongoDbService>();
        services.AddSingleton<IMongoOnlyService, MongoOnlyService>();
        // MongoDbServiceLibrary end
        
        CurrencyClient.Initialize(apiDefaultString);

        //quartz start
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            q.AddJobAndTrigger<FetchDataFromApiJob>(hostContext.Configuration);
        });
        
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        //quartz end
    })
    .Build();

await host.RunAsync();


