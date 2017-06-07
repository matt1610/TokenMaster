namespace TokenMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StandsandDevices : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventDevices",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EventStands", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.EventStands",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EventModels", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventStands", "Id", "dbo.EventModels");
            DropForeignKey("dbo.EventDevices", "Id", "dbo.EventStands");
            DropIndex("dbo.EventStands", new[] { "Id" });
            DropIndex("dbo.EventDevices", new[] { "Id" });
            DropTable("dbo.EventStands");
            DropTable("dbo.EventDevices");
        }
    }
}
