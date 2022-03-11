using Clinic.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Common.Helpers.API
{
    public interface IMessageHelper
    {

        public Task<Message> PostMessage(string userID, string message);

   
    }
}
