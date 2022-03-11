using Clinic.Common.ResponseModels;
using Clinic.Common.RRModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Common.Helpers.API
{
    public interface IDoctorHelper
    {
        public Task<List<DoctorInfoResponse>> GetAllDoctors();

        public Task<DoctorInfoResponse> GetDoctorByID(string id);

        public Task <List<String>> GetDoctorAvailSlots(string id);

        public Task <List<DoctorSlotsResponse>> GetAllDoctorsAvailSlots();

        public Task<List<DoctorSlotsResponse>> DoctorMostSlots();

        public Task <List<DoctorSlotsResponse>> DoctorSixHours();






    }
}
