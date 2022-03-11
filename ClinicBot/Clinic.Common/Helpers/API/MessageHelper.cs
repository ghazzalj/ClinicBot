using Clinic.Common.Context;
using Clinic.Common.Helpers.Bot;
using Clinic.Common.Models;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Common.Helpers.API
{
    public class MessageHelper: IMessageHelper
    {


        private readonly MeetingsContext context;
        private readonly IBotFrameworkHttpAdapter _adapter;
        private readonly IConfiguration _configuration;
        public MessageHelper(MeetingsContext context, IBotFrameworkHttpAdapter adapter, IConfiguration configuration)
        {
            this.context = context;
            _adapter = adapter;
            _configuration = configuration;
        }

     

        public async Task<Message> PostMessage(string userID, string message)
        {
            Message m = new Message();

            m.Content = message;
            m.UserID = userID;

            ConversationRef conRef = new ConversationRef();

            conRef.UPN = context.Conv.Where(x => x.Id.Equals(userID)).Select(x => x.UPN).Single();
            conRef.ConvID = context.Conv.Where(x => x.Id.Equals(userID)).Select(x => x.ConvID).Single();
            conRef.ServiceUrl = context.Conv.Where(x => x.Id.Equals(userID)).Select(x => x.ServiceUrl).Single();
            conRef.ActivityID = context.Conv.Where(x => x.Id.Equals(userID)).Select(x => x.ActivityID).Single();

            if (conRef != null)
            {
                ConversationReference reference = new ()
                {
                    Conversation = new ConversationAccount()
                    {
                        Id = conRef.ConvID
                    },
                    ServiceUrl = conRef.ServiceUrl,


                };



                await ((BotAdapter)_adapter).ContinueConversationAsync(
                       _configuration["MicrosoftAppId"],
                       reference,
                       async (context, token) =>
                       {

                           var attachment = MessageFactory.Attachment(AdaptiveCardHelper.GetNotificationAdaptiveCard(m));
                           // attachment.Summary = entity.ShortDescription;

                            conRef.ActivityID = await BotCallback(attachment, context, token);


                       },
                       default);



            }





            return m;



        }

        private static async Task<string> BotCallback(
           IMessageActivity message,
           ITurnContext turnContext,
           CancellationToken cancellationToken)
        {
            return (await turnContext.SendActivityAsync(message, cancellationToken)).Id;
        }




    }
}
