using Clinic.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Common.ResponseModels
{
    public class PatientResponse
    {
        public string Id { get; set; }
        public List<Appointment> History { get; set; }
    }
}
