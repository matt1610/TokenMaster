namespace TokenMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeviceID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "DeviceId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "DeviceId");
        }
    }
}
