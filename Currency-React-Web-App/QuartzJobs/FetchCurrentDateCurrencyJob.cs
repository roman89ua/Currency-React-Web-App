using System.Globalization;
using Currency_React_Web_App.Clients;
using Quartz;

namespace Currency_React_Web_App.QuartzJobs;

// @DisallowedConcurrentExecution
public abstract class FetchCurrentDateCurrencyJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
       Console.WriteLine($"From execute Function {DateTime.Now.ToString(CultureInfo.InvariantCulture)} ");
       await CurrencyClient.DataBaseRefresh();
    }
}