using Currency_React_Web_App.QuartzJobs;
using Microsoft.AspNetCore.Mvc;
using Quartz;

namespace Currency_React_Web_App.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : Controller
{
    private readonly IScheduler _scheduler;
    
    public  HomeController (IScheduler scheduler)
    {
        _scheduler = scheduler;
    }

    public Task Index()
    {
        return Task.CompletedTask;
    }
    
    [HttpGet]
    [Route("updatedbonappatart")]
    public async Task UpdateDbOnAppStart()
    {
        Console.WriteLine("UPDATED!");
        try
        {

            IJobDetail updateCurrencyDbJob = JobBuilder
                .Create<FetchCurrentDateCurrencyJob>()
                .WithIdentity("DatabaseRefresh", "Fetch")
                .Build();
            
            Console.WriteLine(updateCurrencyDbJob);
            
            ITrigger currencyDbTrigger = TriggerBuilder.Create()
                    .WithIdentity("DatabaseRefreshTrigger", "Fetch")
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).WithRepeatCount(5))
                    .Build();

            Console.WriteLine(currencyDbTrigger);
            await _scheduler.ScheduleJob(updateCurrencyDbJob, currencyDbTrigger);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}