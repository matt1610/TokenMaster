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
    public class EventStandsController : ApiController
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

        // GET: api/EventStands
        public IQueryable<EventStand> GetEventStands()
        {
            return db.EventStands;
        }

        // GET: api/EventStands/5
        [ResponseType(typeof(EventStand))]
        public async Task<IHttpActionResult> GetEventStand(Guid id)
        {
            EventStand eventStand = await db.EventStands.FindAsync(id);
            if (eventStand == null)
            {
                return NotFound();
            }

            return Ok(eventStand);
        }

        // PUT: api/EventStands/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEventStand(Guid id, EventStand eventStand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventStand.Id)
            {
                return BadRequest();
            }

            db.Entry(eventStand).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventStandExists(id))
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

        // POST: api/EventStands
        [ResponseType(typeof(EventStand))]
        [Authorize]
        public async Task<ApiResponse> PostEventStand(EventStand eventStand)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResponse(false, "Model in invalid...");
            }

            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if ( !user.MyCreatedEvents.Contains( new Guid(eventStand.EventId)) )
            {
                return new ApiResponse(false, "You cannot add a stand to this event....");
            }
            
            db.EventStands.Add(eventStand);

            try
            {
                if (await db.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, "Event Stand Created...");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

             return new ApiResponse(false, "Something went wrong...");
        }

        // DELETE: api/EventStands/5
        [ResponseType(typeof(EventStand))]
        public async Task<IHttpActionResult> DeleteEventStand(Guid id)
        {
            EventStand eventStand = await db.EventStands.FindAsync(id);
            if (eventStand == null)
            {
                return NotFound();
            }

            db.EventStands.Remove(eventStand);
            await db.SaveChangesAsync();

            return Ok(eventStand);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventStandExists(Guid id)
        {
            return db.EventStands.Count(e => e.Id == id) > 0;
        }
    }
}