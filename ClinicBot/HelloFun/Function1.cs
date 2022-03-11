using System;
using System.Threading.Tasks;
using Clinic.Common.Context;
using Clinic.Common.Helpers.API;
using Clinic.Common.Models;
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

    public interface IWelcomeHelper
    {

        public  Task<Message> SendWelcomeCard();
    }
    public class WelcomeHelper : IWelcomeHelper
    {
        private readonly IMessageHelper _botNotification;
        private readonly IConfiguration _configuration;

        private readonly MeetingsContext _context;

        public WelcomeHelper(IMessageHelper botNotification,
          IConfiguration configuration,
        MeetingsContext context
          )
        {
            _botNotification = botNotification;
            _configuration = configuration;
            _context = context;
        
        }
        public async Task<Message> SendWelcomeCard() {


            return await _botNotification.PostMessage("29:1LYR4p9zi_fJClDkk6AhQ7W5ybvitpCiT_3pNxQS1okeltDHkZqY50ZG_UQGX4g9AU2Zq6lxUWShrzQDp8Q5VjQ", "Hello");
        
        
        
        
        }



    }
    public static class Function1
    {
     
        [FunctionName("Function1")]
        public static void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
           

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");



            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IWelcomeHelper, WelcomeHelper>()
                .AddSingleton<IMessageHelper, MessageHelper>()
                // Create the Bot Framework Adapter with error handling enabled.
                .AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>()
                .BuildServiceProvider();

            var welcomeHelper = serviceProvider.GetService<IWelcomeHelper>();
             welcomeHelper.SendWelcomeCard();
            // var messHelper = new MessageHelper();
            //messHelper.PostMessage("29:1LYR4p9zi_fJClDkk6AhQ7W5ybvitpCiT_3pNxQS1okeltDHkZqY50ZG_UQGX4g9AU2Zq6lxUWShrzQDp8Q5VjQ","Hello");







        }
    }
}
