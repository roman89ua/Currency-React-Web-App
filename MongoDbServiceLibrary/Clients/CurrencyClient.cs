using System.Net.Http.Headers;

namespace MongoDbServiceLibrary.Clients
{
    public class CurrencyClient : ICurrencyClient
    {
        public static HttpClient Client { get; private set; } = new ();
        public static void Initialize(string currentCurrencyUrl)
        {
            Client = new ();
            Client.BaseAddress = new Uri(currentCurrencyUrl);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
    public interface ICurrencyClient{}
}

