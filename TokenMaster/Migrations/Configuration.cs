using System.Data.Entity.Validation;
using System.Text;
using TokenMaster.Models;

namespace TokenMaster.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TokenMaster.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TokenMaster.Models.ApplicationDbContext";
        }

        protected override void Seed(TokenMaster.Models.ApplicationDbContext context)
        {

            try
            {
                #region Event Models

                var eventModel1 = new EventModel
                {
                    AddressVenue = "The Address",
                    Currency = "ZAR",
                    DateTime = DateTime.Now.AddDays(7),
                    Description = "This is the description",
                    EventName = "This is the event name",
                    Location = "The location"

                };

                context.EventModels.AddOrUpdate(em => em.Id,
                        eventModel1
                    );

                context.SaveChanges();

                #endregion

                var eventStand1 = new EventStand
                {
                    Name = "Test Stand",
                    EventId = eventModel1.Id
                };
                context.EventStands.AddOrUpdate(es => es.Id, eventStand1);
                context.SaveChanges();


                var eventDevice1 = new EventDevice()
                {
                    StandId = eventStand1.Id,
                    Name = "My Device"
                };
                context.EventDevices.AddOrUpdate(es => es.Id, eventDevice1);
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }
    }
}
