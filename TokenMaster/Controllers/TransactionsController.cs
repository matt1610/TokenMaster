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
    public class TransactionsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
        private ApplicationUserManager _userManager;

        // GET: api/Transactions
        public IQueryable<Transaction> GetTransactions()
        {
            return db.Transactions;
        }

        // GET: api/Transactions/5
        [ResponseType(typeof(Transaction))]
        public async Task<IHttpActionResult> GetTransaction(Guid id)
        {
            Transaction transaction = await db.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

       

        // POST: api/Transactions
        [ResponseType(typeof(TransactionResponse))]
        [Authorize]
        public async Task<TransactionResponse> PostTransaction(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return new TransactionResponse(false, transaction, "Model state is incorrect...");
            }

            // Get the event the user is attempting to make a transaction against.
            EventModel theEvent = await db.EventModels.FirstOrDefaultAsync(em => em.Id == transaction.EventId);

            if (theEvent == null)
            {
                return new TransactionResponse(false, transaction, "Event does not exist.");
            }

            // Set the Date and User ID of this potential transaction.
            transaction.UserId = User.Identity.GetUserId();
            transaction.TransactionDate = DateTime.Now;


            // Get the current user object to see if they have joined this event.
            ApplicationUser user = await UserManager.Users.FirstOrDefaultAsync(u => u.Id == transaction.UserId);

            UserEvent userEvent = user.AttendingEvents.FirstOrDefault(e => e.EventGuid == transaction.EventId.ToString());
            int index = user.AttendingEvents.FindIndex(e => e.EventGuid == transaction.EventId.ToString());

            if (userEvent == null)
            {
                return new TransactionResponse(false, transaction, "User has not subscribed to this event..");
            }

            // User does not have enough tokens.
            int potentialRemainingTokens = userEvent.TokenCount - transaction.TokenAmount;
            if (potentialRemainingTokens < 0)
            {
                return new TransactionResponse(false, transaction, "You do not have enough tokens to make this transaction.", userEvent.TokenCount);
            }


            //Deduct tokens from the users token store for THIS event.
            userEvent.TokenCount -= transaction.TokenAmount;

            // Add to the total tokens used for this event.
            theEvent.TokensUsed += transaction.TokenAmount;

            // Re-save this users events with the updated information.
            List<UserEvent> myEvents = user.AttendingEvents;
            myEvents[index] = userEvent;
            user.AttendingEvents = myEvents;

            var res = await UserManager.UpdateAsync(user);

            db.Transactions.Add(transaction);



            db.Entry(theEvent).State = EntityState.Modified;


            if (await db.SaveChangesAsync() > 0 && res.Succeeded)
            {
                return new TransactionResponse(true, transaction, "This transaction was successful.", userEvent.TokenCount);
            }

            return new TransactionResponse(false, transaction, res.Errors.FirstOrDefault());

        }



        private bool EventModelExists(Guid id)
        {
            return db.EventModels.Count(e => e.Id == id) > 0;
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionExists(Guid id)
        {
            return db.Transactions.Count(e => e.Id == id) > 0;
        }
    }
}