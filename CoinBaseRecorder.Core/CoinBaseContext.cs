using CoinBaseRecorder.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBaseRecorder.Core
{
    public class CoinBaseContext: DbContext
    {
        public CoinBaseContext()
            : base("name=CoinBaseConnString")
        {
        }
        public virtual DbSet<HistoricalPriceChange> HistoricalPriceChanges { get; set; }
        public virtual DbSet<PriceChange> PriceChanges { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistoricalPriceChange>().HasIndex(x => x.Time);
            modelBuilder.Entity<HistoricalPriceChange>().Property(x => x.Price_Low).HasPrecision(18, 8);
            modelBuilder.Entity<HistoricalPriceChange>().Property(x => x.Price_Open).HasPrecision(18, 8);
            modelBuilder.Entity<HistoricalPriceChange>().Property(x => x.Price_Close).HasPrecision(18, 8);
            modelBuilder.Entity<HistoricalPriceChange>().Property(x => x.Volume).HasPrecision(18, 8);


            modelBuilder.Entity<PriceChange>().HasIndex(x => x.Time);
            modelBuilder.Entity<PriceChange>().HasIndex(x => x.ProdId);
            modelBuilder.Entity<PriceChange>().Property(x => x.Price).HasPrecision(18, 8);
            base.OnModelCreating(modelBuilder);
        }
    }
}
