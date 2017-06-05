namespace TokenMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Transations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        EventId = c.Guid(nullable: false),
                        UserId = c.String(),
                        TokenAmount = c.Int(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.EventModels", "TokensUsed", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventModels", "TokensUsed");
            DropTable("dbo.Transactions");
        }
    }
}
