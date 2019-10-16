namespace CoinBaseRecorder.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PriceChanges",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProdId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 8),
                        Time = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ProdId)
                .Index(t => t.Time);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.PriceChanges", new[] { "Time" });
            DropIndex("dbo.PriceChanges", new[] { "ProdId" });
            DropTable("dbo.PriceChanges");
        }
    }
}
