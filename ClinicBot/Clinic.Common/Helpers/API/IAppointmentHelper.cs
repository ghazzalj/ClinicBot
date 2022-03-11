using Clinic.Common.Models;
using Clinic.Common.ResponseModels;
using Clinic.Common.RRModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Common.Helpers.API
{
   public interface IAppointmentHelper
    {

        public Task<Appointment> BookSlot(SlotRequest newSlot);
        //public Task CancelSlot(string id);
        public Task<List<Appointment>> ViewDetails(string id);
        public Task <PatientResponse> ViewHistory(string id);


    }
}
