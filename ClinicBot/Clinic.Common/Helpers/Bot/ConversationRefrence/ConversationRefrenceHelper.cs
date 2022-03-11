using Clinic.Common.Context;
using Clinic.Common.Models;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Schema.Teams;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Common.Helpers.Bot.ConversationRefrence
{
    public class ConversationReferencesHelper : IConversationReferencesHelper
    {

        private readonly MeetingsContext context;
        public ConversationReferencesHelper(MeetingsContext context)
        {
            this.context = context;
        }


        public async Task AddorUpdateConversationRefrenceAsync(ConversationReference reference, TeamsChannelAccount member)
        {

            var id = context.Conv.Where(x => x.UPN.Equals(member.UserPrincipalName)).Select(x => x.UPN).FirstOrDefault();
            if (id != member.UserPrincipalName) {
                ConversationRef conversationRef = new ConversationRef();
                conversationRef.Id = member.Id;
                conversationRef.UPN = member.UserPrincipalName;
                conversationRef.ConvID = reference.Conversation.Id;
                conversationRef.ServiceUrl = reference.ServiceUrl;
                conversationRef.ActivityID = reference.ActivityId;
                context.Conv.Add(conversationRef);
                await context.SaveChangesAsync();

            }
         

        }

        public async Task DeleteConversationRefrenceAsync(ConversationReference reference, TeamsChannelAccount member)
        {


            ConversationRef conv = await GetConversationRefrenceAsync(member.UserPrincipalName);
            context.Conv.Attach(conv);
            context.Conv.Remove(conv);
            context.SaveChanges();
           
        }

        public async Task<ConversationRef> GetConversationRefrenceAsync(string upn)
        {

            ConversationRef conversationRef = new ConversationRef();
            conversationRef.Id = context.Conv.Where(x => x.UPN.Equals(upn)).Select(i=> i.Id).Single();
            conversationRef.UPN = upn;
            conversationRef.ConvID = context.Conv.Where(x => x.UPN.Equals(upn)).Select(i => i.ConvID).Single();

            return conversationRef;


            //  return await 


        }
    }
}
