using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using TokenMaster.Models;
using TokenMaster.Singletons;

namespace TokenMaster.Hubs
{
    public class TokenHub : Hub
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private const bool DEBUG = true;

        public void Hello()
        {
            Clients.All.hello();
        }

        public void RegisterEventDevice(string eventGuid, string standId, string deviceId)
        {
            try
            {
                EventStand eventStand = db.EventStands.FirstOrDefault(es => es.Id.ToString() == standId);
                EventClientsManager.Instance.AddClient(new EventDeviceClient(eventGuid, Context.ConnectionId, eventStand, deviceId));

                dynamic response = new
                {
                    Msg = "Connected. Awaiting Transations..",
                    Success = true
                };
                Clients.Caller.registerSuccess(JsonConvert.SerializeObject(response));
            }
            catch (Exception e)
            {
                dynamic response = new
                {
                    Msg = e.Message + " ------------------ Most likely incorrect event id, stand id, or device id...",
                    Success = false
                };
                Clients.Caller.registerSuccess(JsonConvert.SerializeObject(response));
                throw;
            }
        }

        public void GETDEBUGDETAILS()
        {
            try
            {
                EventModel evt =
                db.EventModels.FirstOrDefault( em => em.EventStands.Count > 0 );
                EventStand stand = evt.EventStands.FirstOrDefault(st => st.EventDevices.Count > 0);
                EventDevice device = stand.EventDevices.FirstOrDefault();
                

                if (DEBUG)
                {
                    if (evt != null && stand != null && device != null)
                    {
                        Clients.Caller.RETURNDEBUGDETAILS(
                        evt.Id,
                        stand.Id,
                        device.Id
                        );
                    }
                    else
                    {
                        Clients.Caller.RETURNDEBUGDETAILS("N/A", "N/A", "N/A");
                    }

                }


            }
            catch (Exception e)
            {
                Clients.Caller.RETURNDEBUGDETAILS("N/A", "N/A", "N/A");
            }

            
        }

        public void SendSuccessToEventClient(string socketId, Transaction transaction)
        {
            Clients.Client(socketId).receiveSuccessfullTransaction(transaction.TokenAmount);
        }
    }
}