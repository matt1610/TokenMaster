using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using TokenMaster.Models;
using TokenMaster.Singletons;

namespace TokenMaster.Hubs
{
    public class TokenHub : Hub
    {
        
        public void Hello()
        {
            Clients.All.hello();
            
        }

        public void RegisterEventDevice(string eventGuid)
        {
            EventClientsManager.Instance.AddClient(new EventDeviceClient( eventGuid, Context.ConnectionId));
            Clients.Caller.registerSuccess(true);
        }

        public void SendSuccessToEventClient(string socketId, Transaction transaction)
        {
            Clients.Client(socketId).receiveSuccessfullTransaction(transaction.TokenAmount);
        }
    }
}