using Clinic.Common.Models;
using Clinic.Common.RRModels;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Common.Helpers.Bot
{
   public class AdaptiveCardHelper
    {

        public static Attachment GetDoctorCard(DoctorInfoResponse input)
        {
            
                HeroCard heroCard = new()
                {
                    Title = "Doctor",
                    Subtitle = input.Username +" " + input.DoctorId,
                };
                Attachment Attachment = heroCard.ToAttachment();
            
            
            return Attachment;
        }


        public static Attachment GetNotificationAdaptiveCard(Message input) {


            HeroCard heroCard = new()
            {
                Title = "Message",
                Subtitle = input.Content,
            };
            Attachment Attachment = heroCard.ToAttachment();


            return Attachment;


        }

        public static List<Attachment> GetAllDoctorsCard(List<DoctorInfoResponse> input)
        {
            List<Attachment> attachments = new();
            foreach (var item in input.Take(3))
            {
                HeroCard heroCard = new()
                {
                    Title = "Doctor",
                    Subtitle = item.Username + " " + item.DoctorId,
                };
                Attachment Attachment = heroCard.ToAttachment();
                attachments.Add(Attachment);
            }
            return attachments;
        }



    }
}
