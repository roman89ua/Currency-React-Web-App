using System.Globalization;
using Quartz;

namespace Currency_React_Web_App.QuartzJobs;

public abstract class FetchCurrentDateCurrencyJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
       Console.WriteLine($"From execute Function {DateTime.Now.ToString(CultureInfo.InvariantCulture)} ");
    }
}