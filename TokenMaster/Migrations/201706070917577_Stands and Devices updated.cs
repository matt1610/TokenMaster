namespace TokenMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StandsandDevicesupdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventDevices", "EventId", c => c.String());
            AddColumn("dbo.EventDevices", "StandId", c => c.String());
            AddColumn("dbo.EventStands", "EventId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventStands", "EventId");
            DropColumn("dbo.EventDevices", "StandId");
            DropColumn("dbo.EventDevices", "EventId");
        }
    }
}
