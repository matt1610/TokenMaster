namespace TokenMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.EventDevices");
            AddColumn("dbo.EventDevices", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.EventDevices", "Id");
            DropColumn("dbo.EventDevices", "EdId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EventDevices", "EdId", c => c.Guid(nullable: false, identity: true));
            DropPrimaryKey("dbo.EventDevices");
            DropColumn("dbo.EventDevices", "Id");
            AddPrimaryKey("dbo.EventDevices", "EdId");
        }
    }
}
