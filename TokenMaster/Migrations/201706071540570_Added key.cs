namespace TokenMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedkey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventStands", "Id", "dbo.EventModels");
            DropPrimaryKey("dbo.EventModels");
            AlterColumn("dbo.EventModels", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.EventModels", "Id");
            AddForeignKey("dbo.EventStands", "Id", "dbo.EventModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventStands", "Id", "dbo.EventModels");
            DropPrimaryKey("dbo.EventModels");
            AlterColumn("dbo.EventModels", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.EventModels", "Id");
            AddForeignKey("dbo.EventStands", "Id", "dbo.EventModels", "Id");
        }
    }
}
