using Currency_React_Web_App.Interfaces;
using Currency_React_Web_App.Services;
using MongoDB.Driver;
using LoadDataLibrary;
using LoadDataLibrary.Clients;
using LoadDataLibrary.Interfaces;

namespace Currency_React_Web_App;

public class Program
{
   public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        
        string currentCurrencyUrl = (builder.Configuration.GetValue<string>("urls:BaseRequestUrl"));
        string mongoClientUri = builder.Configuration.GetValue<string>("urls:MongoDBUri");
        
        CurrencyClient.Initialize(currentCurrencyUrl);
        
        services.AddSingleton<MongoClientBase, MongoClient>(_ => new MongoClient(mongoClientUri));
        
        services.AddSingleton<ICurrentDateCurrencyService, SortDateCurrencyService>();
        
        services.AddSingleton<ILoadDataService, LoadDataService>();
        
        services.AddSingleton<IMongoService, MongoService>();
        
        services.AddSingleton<ICurrentDateCurrencyCache, CurrentDateCurrencyCache>();
        services.AddMemoryCache();
        
        services.AddControllers();


        var app = builder.Build();

        app.UseStaticFiles();
        app.UseRouting();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}"
        );

        app.MapFallbackToFile("index.html");

        app.Run();
    }
}

