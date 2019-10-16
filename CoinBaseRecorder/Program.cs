using CoinbasePro.Shared.Types;
using CoinbasePro.WebSocket.Models.Response;
using CoinbasePro.WebSocket.Types;
using CoinBaseRecorder.DAL;
using CoinBaseRecorder.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoinBaseRecorder
{
    class Program
    {
        private static readonly CoinBaseContext _context = new CoinBaseContext();

        private static void Main(string[] args)
        {
            var coinbaseProClient = new CoinbasePro.CoinbaseProClient();

            var productTypes = Enum.GetValues(typeof(ProductType)).Cast<ProductType>().ToList();
            var channels = new List<ChannelType>() { ChannelType.Ticker }; // When not providing any channels, the socket will subscribe to all channels

            var webSocket = coinbaseProClient.WebSocket;
            webSocket.OnTickerReceived += WebSocket_OnTickerReceived;
            webSocket.Start(productTypes, channels);

            SavePeriodically();

            Console.ReadLine();

            webSocket.Stop();
            _context.SaveChanges();
        }

        private static void WebSocket_OnTickerReceived(object sender, WebfeedEventArgs<Ticker> e)
        {
            var priceChangeObj = new PriceChange
            {
                Id = Guid.NewGuid(),
                ProdId = (int)e.LastOrder.ProductId,
                Price = e.LastOrder.Price,
                Time = e.LastOrder.Time
            };
            _context.PriceChanges.Add(priceChangeObj);
            Console.WriteLine(priceChangeObj.Time.ToString() + '\t' + priceChangeObj.ProdId + '\t' + priceChangeObj.Price);
        }

        private static void SavePeriodically()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(10000);
                    _context.SaveChanges();
                }
            });
        }
    }
}
