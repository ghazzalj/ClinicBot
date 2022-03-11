using Clinic.Common.Helpers.API;
using Clinic.Common.Helpers.Bot.ConversationRefrence;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Teams;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;

namespace Clinic.Api.Bots
{
    public class BotTest : ActivityHandler
    {


        private readonly IDoctorHelper _doctorHelper;
        private readonly IConversationReferencesHelper _conversationRefrencesHelper;
        private readonly IMessageHelper _messageHelper;

        public BotTest(IDoctorHelper doctorHelper, IConversationReferencesHelper conversationHelper, IMessageHelper messageHelper)
        {

            _doctorHelper = doctorHelper;
            _conversationRefrencesHelper = conversationHelper;
            _messageHelper = messageHelper;
        }


        protected override async Task OnConversationUpdateActivityAsync(ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            ConversationReference botConRef = turnContext.Activity.GetConversationReference();
            var currentMember = await TeamsInfo.GetMemberAsync(turnContext, turnContext.Activity.From.Id, cancellationToken);
            //await _conversationRefrencesHelper.AddorUpdateConversationRefrenceAsync(botConRef, currentMember);
        }
        protected override async Task OnInstallationUpdateActivityAsync(ITurnContext<IInstallationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var activity = turnContext.Activity;

            ConversationReference botConRef = turnContext.Activity.GetConversationReference();
            var currentMember = await TeamsInfo.GetMemberAsync(turnContext, turnContext.Activity.From.Id, cancellationToken);
            //string jsonString = JsonConvert.SerializeObject(activity);

            if (activity.Action.Equals("add")){
                await _conversationRefrencesHelper.AddorUpdateConversationRefrenceAsync(botConRef, currentMember);
            }


            else if (activity.Action.Equals("remove")) {
                await _conversationRefrencesHelper.DeleteConversationRefrenceAsync(botConRef, currentMember);


            }
            

        }


        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Hello world!"), cancellationToken);
                }
            }
        }




        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var replyText = $"Echo: {turnContext.Activity.Text}";
            await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);



            /* if (turnContext.Activity.Text.ToLower().Contains("doctor list"))
             {




                 var doctors = await _doctorHelper.GetAllDoctors();
                 if (doctors != null)
                 {

                     var res = MessageFactory.Attachment(AdaptiveCardHelper.GetAllDoctorsCard(doctors));
                     res.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                     await turnContext.SendActivityAsync(res, cancellationToken);
                 }
                 else
                     await turnContext.SendActivityAsync(MessageFactory.Text($"No pending mesgs for this user."), cancellationToken);
                 return;


             }

             else
             if (turnContext.Activity.Text.ToLower().Contains("doctor"))
             {
                 //  var approvals = (await _approvalHelper.GetApprovalsAsync(emailFromBot: currentMember.Email.ToLower()))?.entries;
                 /* var doctors = 
                    if (approvals != null && approvals.Any())
                    {
                        var res = MessageFactory.Attachment(AdaptiveCardHelper.GetApprovalsHeroCardsActivity(approvals));
                        res.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                        await turnContext.SendActivityAsync(res, cancellationToken);
                    }
                    else
                        await turnContext.SendActivityAsync(MessageFactory.Text($"No pending approvals for this user."), cancellationToken);
                    return;
                */
            // await turnContext.SendActivityAsync(MessageFactory.Text($"enter the dr's ID"), cancellationToken);
            // string id  = turnContext.Activity.Text;

            /////

            /*   var doctors =  await _doctorHelper.GetDoctorByID("38531a4c-141f-46d2-b352-5833d33619a1");
               if (doctors != null)
               {

                   var res = MessageFactory.Attachment(AdaptiveCardHelper.GetDoctorCard(doctors));
                  // res.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                   await turnContext.SendActivityAsync(res, cancellationToken);
               }
               else
                   await turnContext.SendActivityAsync(MessageFactory.Text($"No pending mesgs for this user."), cancellationToken);
               return;

           }*/









        }




    }


}

