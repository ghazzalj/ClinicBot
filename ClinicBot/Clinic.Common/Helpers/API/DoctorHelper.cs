using Clinic.Common.Context;
using Clinic.Common.ResponseModels;
using Clinic.Common.RRModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Common.Helpers.API
{
    public class DoctorHelper : IDoctorHelper
    {
        private readonly MeetingsContext context;
        public DoctorHelper(MeetingsContext context)
        {
            this.context = context;
        }


        public List<String> GetAvail(string id)
        {
            int count = context.Appointment.Where(s => s.DoctorID.Equals(id)).Count();// slot count
            List<DateTime> startDuration = context.Appointment.Where(s => s.DoctorID.Equals(id)).Select(d => d.StartTime).ToList();
            List<DateTime> endDuration = context.Appointment.Where(s => s.DoctorID.Equals(id)).Select(d => d.EndTime).ToList();
            double totalDuration = 0.0;
            List<String> slots = new List<String>();//avail
            List<String> uslots = new List<String>();// unavail
            List<DateTime> sortedSlots = new List<DateTime>();//sorted list

            var duration = startDuration.Zip(endDuration, (s, e) => new { startDuration = s, endDuration = e });
            foreach (var se in duration)
            {
                //get time difference 
                System.TimeSpan timeSpan = se.endDuration.Subtract(se.startDuration);
                double mins = timeSpan.TotalMinutes;
                totalDuration += mins; // total of mins allocated from all appointments 

                string unAvailSlot = se.startDuration.ToString("H:mm") + "-" + se.endDuration.ToString("H:mm");
                uslots.Add(unAvailSlot);

                //storing slots in a list (start,end)
                sortedSlots.Add(se.startDuration);
                sortedSlots.Add(se.endDuration);

            }

            // sort the list
            sortedSlots.Sort((a, b) => a.CompareTo(b));

            // if endtime1 != starttime2 then avail slot is endtime1-starttime

            for (int i = 0; i < sortedSlots.Count / 2; i++)
            {

                if (sortedSlots[i + 1] != sortedSlots[i + 2])
                {
                    if (count != 12)
                    {
                        string slot = sortedSlots[i + 1].ToString("H:mm") + "-" + sortedSlots[i + 2].ToString("H:mm");
                        slots.Add(slot);
                        count++;

                    }
                }

            }
            //to get the time in hrs
            totalDuration /= 60;

            if (count == 12 || totalDuration >= 8)
                return uslots;

            else
                return slots;


        }

       public List<DoctorSlotsResponse> GetAll() {


            List<string> ids = context.Appointment.Select(x => x.DoctorID).Distinct().ToList();
            int totalDr = ids.Count();

            List<DoctorSlotsResponse> response = new List<DoctorSlotsResponse>();

            for (int i = 0; i < ids.Count; i++)
            {

                DoctorSlotsResponse dr = new DoctorSlotsResponse();
                dr.DoctorId = ids[i];
                dr.Slots = GetAvail(ids[i]);
                response.Add(dr);
            }
            return response;



        }

       public List<DoctorSlotsResponse> SortedAll() {


            List<DoctorSlotsResponse> dr = GetAll();

            dr.Sort(delegate (DoctorSlotsResponse x, DoctorSlotsResponse y) {
                return y.Slots.Count().CompareTo(x.Slots.Count());
            });

            return dr;


        }

        public async Task<List<DoctorInfoResponse>> GetAllDoctors()
        {

            string drRoleID = context.Roles.Where(d => d.Name.Equals("Doctor")).Select(x => x.Id).Single();
            List<String> drIds = context.UserRoles.Where(d => d.RoleId.Equals(drRoleID)).Select(x => x.UserId).ToList();
            List<DoctorInfoResponse> drs = new List<DoctorInfoResponse>();


            foreach (var n in drIds)
            {
                DoctorInfoResponse dr = new DoctorInfoResponse();
                dr.DoctorId = n;
                dr.Username = context.Users.Where(d => d.Id.Equals(n)).Select(x => x.UserName).Single();
                drs.Add(dr);
            }


            return drs;



        }


        /* [HttpGet]
         public async Task List<DoctorInfoResponse> GetAllDoctorsTest()
          {

             DoctorInfoResponse dr = new DoctorInfoResponse();

             string drRoleID = context.Roles.Where(d => d.Name.Equals("Doctor")).Select(x => x.Id).Single();
             List<String> drIds = context.UserRoles.Where(d => d.RoleId.Equals(drRoleID)).Select(x => x.UserId).ToList();
             List<DoctorInfoResponse> drs = new List<DoctorInfoResponse>();



             //context.Users.Where();

             /* foreach (var n in drIds)
              {

                 drs.Add(context.Users.Where(d => d.Id.Equals(n)).Select(x => new DoctorInfoResponse { x.Id  }));
              }
             */


        //    return dr;

        // return await drs;



        // }


        //Doctor information by ID
        //use find because we are returning 1 doctor
   
        public async Task<DoctorInfoResponse> GetDoctorByID(string id)
        { /////////////////////// make a new response model?

            DoctorInfoResponse resp = new DoctorInfoResponse();
            resp.DoctorId = id;
            resp.Username = context.Users.Where(d => d.Id.Equals(id)).Select(x => x.UserName).Single();
            //resp.Name = context.Users.Where(d => d.Id.Equals(id)).Select(x => x.UserName).Single();
            // resp.Slots = context.Appointments.Where(d => d.Id.Equals(id)).ToList();
            return resp;

        }

        //Doctor avail slots
        //get count and add duration, if the count is >12 or the duration is >=8 then no slots
        // 15 , 30, 45, 60, 75, 90, 105, 120

       
        public async Task <List<String>> GetDoctorAvailSlots(string id)
        {


            return GetAvail(id);

            

        }

        //All doctors that are avail

        public async Task<List<DoctorSlotsResponse>> GetAllDoctorsAvailSlots()
        {
            return GetAll();
          

        }


        //Doctors with the most appointments 

   
        public async Task <List<DoctorSlotsResponse>> DoctorMostSlots()
        {
            return SortedAll();
           

        }


        //Doctors who have 6+ hours

   
        public async Task<List<DoctorSlotsResponse>> DoctorSixHours()
        {

            List<DoctorSlotsResponse> dr = SortedAll();

            List<DoctorSlotsResponse> sixHoursList = new List<DoctorSlotsResponse>();

            for (int i = 0; i < dr.Count(); i++)
            {

                if (dr[i].Slots.Count() > 6)
                {

                    sixHoursList.Add(dr[i]);
                }

            }


            return sixHoursList;

        }

    }
}
