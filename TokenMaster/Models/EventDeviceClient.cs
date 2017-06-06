using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenMaster.Models
{
    public class EventDeviceClient
    {
        public string EventId { get; set; }
        public string SocketId { get; set; }

        public EventDeviceClient(string eventId, string socketId)
        {
            SocketId = socketId;
            EventId = eventId;
        }
    }
}
