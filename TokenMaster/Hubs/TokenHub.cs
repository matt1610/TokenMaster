using System;
using System.Collections.Generic;
using System.Data.Entity;
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


                int clientIndex = EventClientsManager.Instance.EventDeviceClients.FindIndex(ec => ec.DeviceId == deviceId);
                EventDeviceClient newClient = new EventDeviceClient(eventGuid, Context.ConnectionId, eventStand, deviceId);
                if (clientIndex > -1)
                {
                    EventClientsManager.Instance.EventDeviceClients[clientIndex] = newClient;
                }

                EventClientsManager.Instance.AddClient(newClient);

                dynamic response = new
                {
                    Msg = "Connected. Awaiting Transactions..",
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

                EventModel eventDeviceDb = db
                    .EventModels
                    .Include(em => em.EventStands.Select(es => es.EventDevices))
                    .FirstOrDefault();


                if (DEBUG)
                {
                    if (eventDeviceDb != null)
                    {
                        Clients.Caller.RETURNDEBUGDETAILS(
                        eventDeviceDb.Id,
                        eventDeviceDb.EventStands.FirstOrDefault()?.Id,
                        eventDeviceDb.EventStands.FirstOrDefault()?.EventDevices.FirstOrDefault()?.Id
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