namespace TokenMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedkey1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventStands", "Id", "dbo.EventModels");
            DropIndex("dbo.EventStands", new[] { "Id" });
            AddColumn("dbo.EventStands", "EventModel_Id", c => c.Guid());
            CreateIndex("dbo.EventStands", "EventModel_Id");
            AddForeignKey("dbo.EventStands", "EventModel_Id", "dbo.EventModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventStands", "EventModel_Id", "dbo.EventModels");
            DropIndex("dbo.EventStands", new[] { "EventModel_Id" });
            DropColumn("dbo.EventStands", "EventModel_Id");
            CreateIndex("dbo.EventStands", "Id");
            AddForeignKey("dbo.EventStands", "Id", "dbo.EventModels", "Id");
        }
    }
}
