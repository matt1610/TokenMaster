using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenMaster.Models;

namespace TokenMaster.Singletons
{
    public class EventClientsManager : Singleton<EventClientsManager>
    {
        public List<EventDeviceClient> EventDeviceClients { get; set; }

        public void AddClient(EventDeviceClient client)
        {
            EventDeviceClients.Add(client);
        }
    }
}