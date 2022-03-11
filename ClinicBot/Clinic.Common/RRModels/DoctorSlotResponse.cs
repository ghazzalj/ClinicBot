using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Common.ResponseModels
{
    public class DoctorSlotsResponse
    {
        public string DoctorId { get; set; }
        public List<String> Slots { get; set; }


    }
}
