using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using TokenMaster.Models;

namespace TokenMaster.Controllers
{
    public class EventDevicesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: api/EventDevices
        public IQueryable<EventDevice> GetEventDevices()
        {
            return db.EventDevices;
        }

        // GET: api/EventDevices/5
        [ResponseType(typeof(EventDevice))]
        public async Task<IHttpActionResult> GetEventDevice(Guid id)
        {
            EventDevice eventDevice = await db.EventDevices.FindAsync(id);
            if (eventDevice == null)
            {
                return NotFound();
            }

            return Ok(eventDevice);
        }

        // PUT: api/EventDevices/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEventDevice(Guid id, EventDevice eventDevice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventDevice.Id)
            {
                return BadRequest();
            }

            db.Entry(eventDevice).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventDeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EventDevices
        //[ResponseType(typeof(ApiResponse))]
        public async Task<ApiResponse> PostEventDevice(EventDevice eventDevice)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResponse(false, "Model state in invalid....");
            }

            if (db.EventModels.FirstOrDefault(em => em.Id == eventDevice.EventId) == null)
            {
                return new ApiResponse(false, "Event does not exist....");
            }

            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if ( !user.MyCreatedEvents.Contains(new Guid(eventDevice.EventId.ToString())) )
            {
                return new ApiResponse(false, "You cannot add a stand to this event....");
            }

            db.EventDevices.Add(eventDevice);

            if (await db.SaveChangesAsync() > 0)
            {
                return new ApiResponse(true, "Event device saved..");
            }

            return new ApiResponse(false, "Something went wrong..");
        }

        // DELETE: api/EventDevices/5
        [ResponseType(typeof(EventDevice))]
        public async Task<IHttpActionResult> DeleteEventDevice(Guid id)
        {
            EventDevice eventDevice = await db.EventDevices.FindAsync(id);
            if (eventDevice == null)
            {
                return NotFound();
            }

            db.EventDevices.Remove(eventDevice);
            await db.SaveChangesAsync();

            return Ok(eventDevice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventDeviceExists(Guid id)
        {
            return db.EventDevices.Count(e => e.Id == id) > 0;
        }
    }
}