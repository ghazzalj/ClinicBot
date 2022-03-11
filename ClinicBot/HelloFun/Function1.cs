using System;
using Clinic.Common.Helpers.API;
using Clinics.Bot;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HelloFun
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
           

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");



            IConfiguration configuration = new ConfigurationBuilder()
                        .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                        .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build();

            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton(configuration)
                .AddSingleton<IMessageHelper, MessageHelper>()
                // Create the Bot Framework Adapter with error handling enabled.
                .AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>()
                .BuildServiceProvider();


         
               // log.LogInformation(transactionId.ToString(), "In WelcomeFunction - SendWelcomeCard()");
                var messHelper = serviceProvider.GetService<IMessageHelper>();
                messHelper.PostMessage("29:1LYR4p9zi_fJClDkk6AhQ7W5ybvitpCiT_3pNxQS1okeltDHkZqY50ZG_UQGX4g9AU2Zq6lxUWShrzQDp8Q5VjQ","Hello");
                
          
         




        }
    }
}
