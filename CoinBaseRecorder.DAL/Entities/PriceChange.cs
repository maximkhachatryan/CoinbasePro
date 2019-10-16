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
        public double Price { get; set; }
        public DateTime Time { get; set; }
    }
}
