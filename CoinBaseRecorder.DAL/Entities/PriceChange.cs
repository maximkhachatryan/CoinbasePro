using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBaseRecorder.DAL.Entities
{
    public class PriceChange
    {
        public Guid Id { get; set; }
        public int ProdId { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
