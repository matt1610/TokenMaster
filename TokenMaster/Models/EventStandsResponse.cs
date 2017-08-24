using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenMaster.Models
{
    public class EventStandsResponse
    {
        public Guid StandId { get; set; }
        public string Name { get; set; }
        //public List<EventDevice> EventDevices { get; set; }

        public EventStandsResponse()
        {

        }
    }
}
