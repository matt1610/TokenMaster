using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenMaster.Models
{
    public class AssignDeviceRequestModel
    {
        public string DeviceId { get; set; }
        public string StandId { get; set; }
        public string EventId { get; set; }
    }
}
