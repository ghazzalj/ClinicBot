﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Common.Models
{
    public class Appointment
    {
        public string Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string DoctorID { get; set; }
        public string PatientID { get; set; }
        public string Status { get; set; }



    }
}
