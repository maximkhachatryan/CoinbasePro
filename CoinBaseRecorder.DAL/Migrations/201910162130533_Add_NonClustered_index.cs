namespace CoinBaseRecorder.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_NonClustered_index : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PriceChanges", "Time");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PriceChanges", new[] { "Time" });
        }
    }
}
