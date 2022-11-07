using System.Collections.Specialized;
using System.Net.Http.Headers;
using Currency_React_Web_App.Clients;
using Currency_React_Web_App.Services;
using MongoDB.Driver;
using Quartz;
using Quartz.Impl;
using System.ServiceProcess;

namespace Currency_React_Web_App;

public class Program
{
    public Program ()
    {
        
    }

    private static IScheduler _quartzScheduler = null!;
    
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        
        string currentCurrencyUrl = builder.Configuration.GetValue<string>("urls:BaseRequestUrl");
        string mongoClientUri = builder.Configuration.GetValue<string>("urls:MongoDBUri");
        
        services.AddHttpClient<ICurrencyClient, CurrencyClient>(client =>
        {
            client.BaseAddress = new Uri(currentCurrencyUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });
        
        services.AddSingleton<MongoClientBase, MongoClient>(_ => new MongoClient(mongoClientUri));
        
        services.AddSingleton<ICurrentDateCurrencyService, CurrentDateCurrencyService>();
        
        services.AddSingleton<IFetchDataService, FetchDataService>();
        
        services.AddSingleton<ICurrentDateCurrencyCache, CurrentDateCurrencyCache>();
        services.AddMemoryCache();
        var a = QuartzConfig();
        
        _quartzScheduler = a;
        services.AddSingleton(provider => _quartzScheduler);

        services.AddControllers();


        var app = builder.Build();

        app.UseStaticFiles();
        app.UseRouting();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}"
        );

        app.MapFallbackToFile("index.html"); ;

        app.Run();
    }

    private static IScheduler QuartzConfig()
    {
        NameValueCollection props = new NameValueCollection
        {
            { "quartz.serializer.type", "binary" },
        };

        StdSchedulerFactory factory = new StdSchedulerFactory(props);
        var scheduler = factory.GetScheduler().Result;

        scheduler.Start().Wait();

        return scheduler;
    }

    private void OnShutDown()
    {
        if (!_quartzScheduler.IsShutdown) _quartzScheduler.Shutdown();
    }
}

