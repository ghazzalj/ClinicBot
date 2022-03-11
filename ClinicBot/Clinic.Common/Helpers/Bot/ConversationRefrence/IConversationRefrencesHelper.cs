using Clinic.Common.Models;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Schema.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Common.Helpers.Bot.ConversationRefrence
{

    public interface IConversationReferencesHelper
    {
        Task AddorUpdateConversationRefrenceAsync(ConversationReference reference, TeamsChannelAccount member);
        //Task DeleteConversationRefrenceAsync(ConversationReference reference, TeamsChannelAccount member);
        Task<ConversationRef> GetConversationRefrenceAsync(string upn);

        public  Task DeleteConversationRefrenceAsync(ConversationReference reference, TeamsChannelAccount member);
    }
}
