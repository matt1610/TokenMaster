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
        public string DeviceId { get; set; }
        public EventStand EventStand { get; set; }

        public EventDeviceClient(string eventId, string socketId, EventStand eventStand, string deviceId)
        {
            SocketId = socketId;
            EventId = eventId;
            EventStand = eventStand;
            DeviceId = deviceId;
        }
    }
}
