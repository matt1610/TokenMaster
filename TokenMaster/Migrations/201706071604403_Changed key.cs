namespace TokenMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedkey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventDevices", "EventId", "dbo.EventStands");
            DropIndex("dbo.EventDevices", new[] { "EventId" });
            AddColumn("dbo.EventDevices", "EventStand_Id", c => c.Guid());
            CreateIndex("dbo.EventDevices", "EventStand_Id");
            AddForeignKey("dbo.EventDevices", "EventStand_Id", "dbo.EventStands", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventDevices", "EventStand_Id", "dbo.EventStands");
            DropIndex("dbo.EventDevices", new[] { "EventStand_Id" });
            DropColumn("dbo.EventDevices", "EventStand_Id");
            CreateIndex("dbo.EventDevices", "EventId");
            AddForeignKey("dbo.EventDevices", "EventId", "dbo.EventStands", "Id", cascadeDelete: true);
        }
    }
}
