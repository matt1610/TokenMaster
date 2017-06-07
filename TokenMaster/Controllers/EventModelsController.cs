using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TokenMaster.Models;

namespace TokenMaster.Controllers
{
    [RoutePrefix("api/events")]
    public class EventModelsController : ApiController
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

        [HttpGet]
        [Route("getevents")]
        // GET: api/EventModels
        public IQueryable<EventModel> GetEventModels()
        {
            return db.EventModels;
        }

        // GET: api/EventModels/5
        [ResponseType(typeof(EventModel))]
        public async Task<IHttpActionResult> GetEventModel(Guid id)
        {
            EventModel eventModel = await db.EventModels.FindAsync(id);
            if (eventModel == null)
            {
                return NotFound();
            }

            return Ok(eventModel);
        }


        /// <summary>
        /// Normal USer Joins Event or updates token amount
        /// </summary>
        /// <param name="joinModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("joinevent")]
        [ResponseType(typeof(ApiResponse))]
        [Authorize]
        public async Task<ApiResponse> JoinOrUpdateEvent([FromBody] JoinEventModel joinModel)
        {
            EventModel evt = await db.EventModels.FirstOrDefaultAsync(e => e.Id.ToString() == joinModel.EventId);
            if (evt == null)
            {
                return new ApiResponse(false, "Event does not exist.");
            }

            if (evt.TokensUsed + joinModel.Tokens > evt.MaxTokens)
            {
                return new ApiResponse(false, String.Format("This amount of tokens is not available, {0} are still available for purchase.", evt.MaxTokens - evt.TokensUsed), new
                {
                    RemainingTokens = evt.MaxTokens - evt.TokensUsed
                });
            }

            string uId = User.Identity.GetUserId();

            ApplicationUser user = await UserManager.Users.FirstOrDefaultAsync(u => u.Id == uId);

            List<UserEvent> userEvents = user.AttendingEvents;
            int index = userEvents.FindIndex(item => item.EventGuid == joinModel.EventId);

            UserEvent userEvent;

            if (index == -1)
            {
                userEvent = new UserEvent(joinModel.EventId, joinModel.Tokens);
                userEvents.Add(userEvent);
            }
            else
            {
                userEvent = new UserEvent(joinModel.EventId, joinModel.Tokens + userEvents[index].TokenCount);
                userEvents[index] = userEvent;
            }

            evt.TokensUsed += joinModel.Tokens;

            user.AttendingEvents = userEvents;

            var res = await UserManager.UpdateAsync(user);

            db.Entry(evt).State = EntityState.Modified;

            if (await db.SaveChangesAsync() > 0 && res.Succeeded)
            {
                return new ApiResponse(true, "Token balance is now " + userEvent.TokenCount.ToString());
            }

            return new ApiResponse(true, res.Errors.FirstOrDefault());
        }



        // PUT: api/EventModels/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEventModel(Guid id, EventModel eventModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventModel.Id)
            {
                return BadRequest();
            }

            db.Entry(eventModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventModelExists(id))
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

        // POST: api/EventModels
        [ResponseType(typeof(EventModel))]
        [Authorize]
        public async Task<IHttpActionResult> PostEventModel(EventModel eventModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            db.EventModels.Add(eventModel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = eventModel.Id }, eventModel);
        }


        [HttpPost]
        [Route("addevent")]
        [Authorize]
        public async Task<ApiResponse> AddEvent([FromBody] EventModel evt)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResponse(false, "Something went wrong...");
            }
            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            evt.EventOwner = User.Identity.GetUserName();
            evt.OwnerId = User.Identity.GetUserId();
            
            db.EventModels.Add(evt);
            int res = await db.SaveChangesAsync();

            user.AddToMyEvents(evt.Id);

            IdentityResult idRes = await UserManager.UpdateAsync(user);

            if (idRes.Succeeded && res > 0)
            {
                return new ApiResponse(true, "Event Created", new
                {
                    EventId = evt.Id
                });
            }
            return new ApiResponse(false, "Something went wrong", new
            {
                EventId = evt.Id
            });

        }


        // DELETE: api/EventModels/5
        [ResponseType(typeof(EventModel))]
        public async Task<IHttpActionResult> DeleteEventModel(Guid id)
        {
            EventModel eventModel = await db.EventModels.FindAsync(id);
            if (eventModel == null)
            {
                return NotFound();
            }

            db.EventModels.Remove(eventModel);
            await db.SaveChangesAsync();

            return Ok(eventModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventModelExists(Guid id)
        {
            return db.EventModels.Count(e => e.Id == id) > 0;
        }
    }
}