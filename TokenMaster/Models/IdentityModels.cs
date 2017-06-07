using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace TokenMaster.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
        
        public string CompanyName { get; set; }
        public string myCreatedEvents { get; set; }
        public bool EventCreator { get; set; }
        public string attendingEvents { get; set; }

        [NotMapped]
        public List<UserEvent> AttendingEvents
        {
            get
            {
                if (attendingEvents != null)
                {
                    return JsonConvert.DeserializeObject<List<UserEvent>>(attendingEvents);
                }
                return new List<UserEvent>();
            }
            set
            {
                attendingEvents = JsonConvert.SerializeObject(value);
            }
        }

        public bool AddToMyEvents(Guid eventGuid)
        {
            List<Guid> eventGuids = MyCreatedEvents;
            eventGuids.Add(eventGuid);
            MyCreatedEvents = eventGuids;
            return true;
        }

        [NotMapped]
        public List<Guid> MyCreatedEvents
        {
            get
            {
                if (myCreatedEvents != null)
                {
                    return JsonConvert.DeserializeObject<List<Guid>>(myCreatedEvents);
                }
                return new List<Guid>();
            }
            set
            {
                myCreatedEvents = JsonConvert.SerializeObject(value);
            }
        }


    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<EventModel> EventModels { get; set; }

        public System.Data.Entity.DbSet<TokenMaster.Models.Transaction> Transactions { get; set; }

        public DbSet<EventDevice> EventDevices { get; set; }
        public DbSet<EventStand> EventStands { get; set; }
    }
}