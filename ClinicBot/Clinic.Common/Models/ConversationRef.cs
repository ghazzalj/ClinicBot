using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Common.Models
{
    public class ConversationRef
    {

        public string Id { get; set; }
        private string _upn;
        public string UPN
        {
            get => _upn;
            set => _upn = value.ToLower();
        }
        public string ConvID { get; set; }

        public string ServiceUrl { get; set; }

        public string ActivityID { get; set; }
    }
}
