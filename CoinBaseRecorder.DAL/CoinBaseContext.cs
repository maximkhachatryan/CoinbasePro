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
        public virtual DbSet<PriceChange> PriceChange { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}
