
using Clinic.Common.Context;
using Clinic.Common.Exceptions;
using Clinic.Common.Helpers;
using Clinic.Common.Helpers.API;
using Clinic.Common.Models;
using Clinic.Common.ResponseModels;
using Clinic.Common.RRModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Api.Controllers
{
    [ApiController]
    [Route("api/slots")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentHelper _appointmentHelper;
        public AppointmentsController(IAppointmentHelper appointmentHelper)
        {
            _appointmentHelper = appointmentHelper;
        }

        //Book an appointment 


        [HttpPost]
        public async Task<IActionResult> PostSlot([FromBody] SlotRequest newSlot)
        {
            var res = await _appointmentHelper.BookSlot(newSlot);
            return res != null ? Ok(res) : NotFound();
        }


       /* //Cancel an appoinment 
        [HttpPatch("cancel/{id}")]
        public async Task CancelSlot(string id)
        {
            



        }
       */



        //View appointment details 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(string id)
        {
            var res = await _appointmentHelper.ViewDetails(id);
            return res != null ? Ok(res) : NotFound();

        }

        //View appointment history 

        [HttpGet("patient/{id}")]
        public async Task<IActionResult> GetHistory(string id)
        {
            var res = await _appointmentHelper.ViewHistory(id);
            return res != null ? Ok(res) : NotFound();

        }






    }
}
