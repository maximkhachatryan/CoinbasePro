using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBaseRecorder.Core.Entities
{
    public class HistoricalPriceChange
    {
        public Guid Id { get; set; }
        public int ProdId { get; set; }
        public decimal? Price_Close { get; set; }
        public decimal? Price_High { get; set; }
        public decimal? Price_Low { get; set; }
        public decimal? Price_Open { get; set; }
        public decimal Volume { get; set; }
        public DateTime Time { get; set; }
    }
}
