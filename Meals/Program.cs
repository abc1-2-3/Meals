using Meals.Services.Quartz;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureServices((hostContext, services) =>
                {
                    services.AddQuartz(q =>
                    {
                        q.UseMicrosoftDependencyInjectionScopedJobFactory();

                        // Create a "key" for the job
                        var jobKey = new JobKey("OrderCheckJob");
                        var FTPjobKey = new JobKey("FTPJob");
                        // Register the job with the DI container
                        q.AddJob<OrderCheckJob>(opts => opts.WithIdentity(jobKey));
                        q.AddJob<FTPjob>(opts => opts.WithIdentity(FTPjobKey));
                        // Create a trigger for the job
                        q.AddTrigger(opts => opts
                            .ForJob(jobKey) // link to the HelloWorldJob
                            .WithIdentity("OrderCheckJob-trigger") // give the trigger a unique name
                            .WithCronSchedule("* 0/30 * * * ?")); // run every 5 seconds
                        q.AddTrigger(opts => opts
                            .ForJob(FTPjobKey) // link to the HelloWorldJob
                            .WithIdentity("FTPjob-trigger") // give the trigger a unique name
                            .WithCronSchedule("* 0/5 * * * ?")); // run every 5 seconds

                    });
                    services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

                });
    }
}
