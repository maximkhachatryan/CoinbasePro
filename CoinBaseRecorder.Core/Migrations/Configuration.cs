namespace CoinBaseRecorder.Core.Migrations
{
    using CoinbasePro;
    using CoinbasePro.Network.Authentication;
    using CoinbasePro.Services.Products.Types;
    using CoinbasePro.Shared.Types;
    using CoinBaseRecorder.Core.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CoinBaseRecorder.Core.CoinBaseContext>
    {

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CoinBaseRecorder.Core.CoinBaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.


            //var authenticator = new Authenticator("1f2af4297a92164703300f0e12af5c98", "Yhpr47afGAikPYf6ehD8Ha0V52LAJKL8JYGx7gXc7FSyZWSgAB5iuSpFqydSHk61OMADht2bVBHdNk3mJXGDDA==", "fjblqdrfwc4");
            //var client = new CoinbaseProClient(authenticator, true);
            //var productTypes = Enum.GetValues(typeof(ProductType)).Cast<ProductType>().ToList();
            //foreach (var prodType in productTypes)
            //{
            //    var list = client.ProductsService.GetHistoricRatesAsync(
            //    prodType,
            //    DateTime.MinValue,
            //    DateTime.Today.AddDays(1),
            //    CandleGranularity.Hour24).Result
            //    .Select(x => new HistoricalPriceChange
            //    {
            //        Id = Guid.NewGuid(),
            //        Price_Close = x.Close,
            //        Price_High = x.High,
            //        Price_Low = x.Low,
            //        Price_Open = x.Open,
            //        ProdId = (int)prodType,
            //        Time = x.Time,
            //        Volume = x.Volume
            //    });

            //    context.HistoricalPriceChanges.AddRange(list);
            //}
            //context.SaveChanges();
        }
    }
}
