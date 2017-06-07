namespace TokenMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedeventIdtoguid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventDevices", "Id", "dbo.EventStands");
            DropIndex("dbo.EventDevices", new[] { "Id" });
            DropColumn("dbo.EventDevices", "EventId");
            RenameColumn(table: "dbo.EventDevices", name: "Id", newName: "EventId");
            AlterColumn("dbo.EventDevices", "EventId", c => c.Guid(nullable: false));
            AlterColumn("dbo.EventDevices", "EventId", c => c.Guid(nullable: false));
            CreateIndex("dbo.EventDevices", "EventId");
            AddForeignKey("dbo.EventDevices", "EventId", "dbo.EventStands", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventDevices", "EventId", "dbo.EventStands");
            DropIndex("dbo.EventDevices", new[] { "EventId" });
            AlterColumn("dbo.EventDevices", "EventId", c => c.String());
            AlterColumn("dbo.EventDevices", "EventId", c => c.Guid(nullable: false, identity: true));
            RenameColumn(table: "dbo.EventDevices", name: "EventId", newName: "Id");
            AddColumn("dbo.EventDevices", "EventId", c => c.String());
            CreateIndex("dbo.EventDevices", "Id");
            AddForeignKey("dbo.EventDevices", "Id", "dbo.EventStands", "Id");
        }
    }
}
