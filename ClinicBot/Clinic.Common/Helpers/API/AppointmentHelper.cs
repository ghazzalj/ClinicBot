using Clinic.Common.Context;
using Clinic.Common.Exceptions;
using Clinic.Common.Models;
using Clinic.Common.ResponseModels;
using Clinic.Common.RRModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Common.Helpers.API
{
    public class AppointmentHelper : IAppointmentHelper
    {

        private readonly MeetingsContext context;

        public AppointmentHelper(MeetingsContext context) {

            this.context = context;
        }
        public async Task<Appointment> BookSlot(SlotRequest newSlot)
        {

            Guid idg = Guid.NewGuid();
            var records = (newSlot).MapProperties<Appointment>();
            records.Id = idg.ToString();
            context.Appointment.Add(records);
            await context.SaveChangesAsync();
            return records;
        }


       /* public async Task CancelSlot(string id)
        {
            try

            {

                Appointment slot = context.Appointment.Where(d => d.Id.Equals(id)).Single();
                if (slot == null)
                    throw new Exception("no slot was found");

                slot.Status = "Cancelled";
                context.Appointment.Update(slot);
                context.SaveChanges();

                var response = new SuccessResponseContent<Appointment>
                {
                    ResultData = slot
                };


                Response.StatusCode = 200;
                Response.ContentType = "application/json";
                await Response.Body.WriteAsync(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
            }
            catch (Exception e)
            {

                var failedResponse = new FailedResponseContent
                {
                    StatusMessage = ResponseContentStatusMessages.ExceptionEncounter,
                    Error = e
                };

                Response.StatusCode = 400;
                Response.ContentType = "application/json";
                await Response.Body.WriteAsync(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(failedResponse)));
            }



        }
       */


        //View appointment details 

        public async Task <List<Appointment>> ViewDetails(string id)
        {
            return context.Appointment.Where(d => d.Id.Equals(id)).ToList();

        }

        //View appointment history 

  
        public async Task <PatientResponse> ViewHistory(string id)
        {
            // get all the patient appointments 
            List<Appointment> patientSlots = context.Appointment.Where(d => d.PatientID.Equals(id)).Where(d => d.Status != "Cancelled").ToList();
            PatientResponse response = new PatientResponse();
            response.Id = id;
            response.History = patientSlots;

            return response;


        }





    }
}
