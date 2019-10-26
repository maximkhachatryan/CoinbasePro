using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBaseRecorder.Core.EventResponseModels
{
    public class OrderRecievedEventArgs : EventArgs
    {
        public OrderRecievedEventArgs(DateTimeOffset time, string productName, decimal price)
        {
            Time = time;
            ProductName = productName;
            Price = price;
        }
        public DateTimeOffset Time { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
