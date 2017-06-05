using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        private string userEvents { get; set; }
        public string CompanyName { get; set; }
        public bool EventCreator { get; set; }
        private string attendingEvents { get; set; }

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

        [NotMapped]
        public List<EventModel> UserEvents
        {
            get
            {
                if (userEvents != null)
                {
                    return JsonConvert.DeserializeObject<List<EventModel>>(userEvents);
                }
                return new List<EventModel>();
            }
            set
            {
                userEvents = JsonConvert.SerializeObject(value);
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
    }
}