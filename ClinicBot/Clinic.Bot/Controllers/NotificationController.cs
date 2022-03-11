using Clinic.Common.Helpers.API;
using Clinic.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Bot.Controllers
{


    [ApiController]
    [Route("api/mess")] 
    public class NotificationController: ControllerBase
    {


        private readonly IMessageHelper _messageHelper;
        public NotificationController(IMessageHelper messageHelper)
        {
            _messageHelper = messageHelper;
        }


        [HttpPost]
        public async Task<IActionResult> PostMessage([FromQuery] string userID, [FromQuery] string message)
        {
            var res = await _messageHelper.PostMessage(userID, message);
            return res != null ? Ok(res) : NotFound();
        }





    }
}
