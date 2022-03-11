using Clinic.Common.Context;
using Clinic.Common.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Clinic.Common.RRModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic.Common.Helpers.API;

namespace Clinic.Api.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorHelper _doctorHelper;

        public DoctorController(IDoctorHelper doctorHelper) {

            _doctorHelper = doctorHelper;

        
   
        }




        [HttpGet]
        public async Task<IActionResult> GetAllDoctors() {

            var res = await _doctorHelper.GetAllDoctors();
            return res != null ? Ok(res) : NotFound();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctor([FromQuery] string id)
        {
            var res = await _doctorHelper.GetDoctorByID(id);
            return res!= null ? Ok(res) : NotFound();
        }


        [HttpGet("{id}/slots")]
        public async Task<IActionResult> GetDoctorSlots(string id) {

            var res = await _doctorHelper.GetDoctorAvailSlots(id);
            return res != null ? Ok(res) : NotFound();

        }


        [HttpGet("avail")]

        public async Task<IActionResult> GetAllDoctorSlots()
        {

            var res = await _doctorHelper.GetAllDoctorsAvailSlots();
            return res != null ? Ok(res) : NotFound();

        }


        [HttpGet("mostSlots")]

        public async Task<IActionResult> GetMostSlots()
        {

            var res = await _doctorHelper.DoctorMostSlots();
            return res != null ? Ok(res) : NotFound();

        }

        [HttpGet("sixHoursPlus")]

        public async Task<IActionResult> GetSixSlots()
        {

            var res = await _doctorHelper.DoctorSixHours();
            return res != null ? Ok(res) : NotFound();

        }







    }
}
