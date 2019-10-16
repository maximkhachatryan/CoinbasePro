using CoinBaseRecorder.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBaseRecorder.DAL
{
    public class CoinBaseContext: DbContext
    {
        public CoinBaseContext()
            : base("name=CoinBaseConnString")
        {
        }
        public virtual DbSet<PriceChange> PriceChanges { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PriceChange>().HasIndex(x => x.Time);
            modelBuilder.Entity<PriceChange>().HasIndex(x => x.ProdId);
            modelBuilder.Entity<PriceChange>().Property(x => x.Price).HasPrecision(18, 8);
            base.OnModelCreating(modelBuilder);
        }
    }
}
