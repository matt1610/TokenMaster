namespace TokenMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedkey4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "StandId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "StandId");
        }
    }
}
