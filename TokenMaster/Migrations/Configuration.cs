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
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //context.People.AddOrUpdate(
            //  p => p.FullName,
            //  new Person { FullName = "Andrew Peters" },
            //  new Person { FullName = "Brice Lambson" },
            //  new Person { FullName = "Rowan Miller" }
            //);

            //context.EventModels.AddOrUpdate( em => em.Id, 
            //        new EventModel()
            //        {
            //            AddressVenue = "The Address",
            //            Currency = "ZAR",
            //            DateTime = DateTime.Now.AddDays(7),
            //            Description = "This is the description",
            //            EventName = "This is the event name",

            //        }
            //    );
            //
        }
    }
}
