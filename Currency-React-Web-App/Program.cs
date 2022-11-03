using System.Net.Http.Headers;
using Currency_React_Web_App.Clients;
using Currency_React_Web_App.Controllers;
using Currency_React_Web_App.Services;
using MongoDB.Driver;

namespace Currency_React_Web_App;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string currentCurrencyUrl = builder.Configuration.GetValue<string>("urls:BaseRequestUrl");
        string mongoClientUri = builder.Configuration.GetValue<string>("urls:MongoDBUri");
        
        builder.Services.AddHttpClient<ICurrencyClient, CurrencyClient>(client =>
        {
            client.BaseAddress = new Uri(currentCurrencyUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });
        builder.Services.AddSingleton<MongoClientBase, MongoClient>(_ => new MongoClient(mongoClientUri));
        builder.Services.AddSingleton<ICurrentDateCurrencyService, CurrentDateCurrencyService>();
        builder.Services.AddSingleton<IFetchDataService, FetchDataService>();
        builder.Services.AddSingleton<ICurrentDateCurrencyCache, CurrentDateCurrencyCache>();
        builder.Services.AddMemoryCache();
        builder.Services.AddControllers();


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
}

