using System.Configuration;
using MongoDB.Driver;

namespace Currency_React_Web_App;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string baseRequestUrl = builder.Configuration.GetValue<string>("Urls:BaseRequestUrl");
        new ApiHelper(baseRequestUrl);

        string mongoClientUri = builder.Configuration.GetValue<string>("urls:MongoDBUri");
        builder.Services.AddSingleton<MongoClientBase, MongoClient>(servise => new MongoClient(mongoClientUri));


        builder.Services.AddControllers();


        var app = builder.Build();

        app.UseStaticFiles();
        app.UseRouting();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}"
        );

        app.MapControllerRoute(
            name: "currencycurrentdate",
            pattern: "currencycurrentdate/{*sortcurrencydata}",
            defaults: new { controller = "CurrencyCurrentDate", action = "SortCurrencyData" }
        );

        app.MapFallbackToFile("index.html"); ;

        app.Run();
    }
}

