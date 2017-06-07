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
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Hello()
        {
            Clients.All.hello();
        }

        public void RegisterEventDevice(string eventGuid, string standId)
        {
            EventStand eventStand = db.EventStands.FirstOrDefault(es => es.Id.ToString() == standId);
            EventClientsManager.Instance.AddClient(new EventDeviceClient( eventGuid, Context.ConnectionId, eventStand));
            Clients.Caller.registerSuccess(true);
        }

        public void SendSuccessToEventClient(string socketId, Transaction transaction)
        {
            Clients.Client(socketId).receiveSuccessfullTransaction(transaction.TokenAmount);
        }
    }
}