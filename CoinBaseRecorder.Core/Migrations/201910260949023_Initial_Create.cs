namespace CoinBaseRecorder.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistoricalPriceChanges",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProdId = c.Int(nullable: false),
                        Price_Close = c.Decimal(precision: 18, scale: 8),
                        Price_High = c.Decimal(precision: 18, scale: 2),
                        Price_Low = c.Decimal(precision: 18, scale: 8),
                        Price_Open = c.Decimal(precision: 18, scale: 8),
                        Volume = c.Decimal(nullable: false, precision: 18, scale: 8),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Time);
            
            CreateTable(
                "dbo.PriceChanges",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TradeId = c.Long(nullable: false),
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
            DropIndex("dbo.HistoricalPriceChanges", new[] { "Time" });
            DropTable("dbo.PriceChanges");
            DropTable("dbo.HistoricalPriceChanges");
        }
    }
}
