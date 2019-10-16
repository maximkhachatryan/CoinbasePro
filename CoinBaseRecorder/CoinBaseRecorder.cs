using CoinbasePro.Shared.Types;
using CoinbasePro.WebSocket;
using CoinbasePro.WebSocket.Models.Response;
using CoinbasePro.WebSocket.Types;
using CoinBaseRecorder.DAL;
using CoinBaseRecorder.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebSocket4Net;

namespace CoinBaseRecorder
{
    class CoinBaseRecorder : IDisposable
    {
        readonly CoinBaseContext _context;
        readonly IWebSocket _webSocket;
        public CoinBaseRecorder()
        {
            _context = new CoinBaseContext();
            var coinbaseProClient = new CoinbasePro.CoinbaseProClient();
            _webSocket = coinbaseProClient.WebSocket;
            _webSocket.OnTickerReceived += WebSocket_OnTickerReceived;

            Console.WriteLine(_webSocket.State);
        }

        public void StartRecording()
        {
            var productTypes = Enum.GetValues(typeof(ProductType)).Cast<ProductType>().ToList();
            var channels = new List<ChannelType>() { ChannelType.Ticker };
            _webSocket.Start(productTypes, channels);
            SavePeriodically();
        }

        private void WebSocket_OnTickerReceived(object sender, WebfeedEventArgs<Ticker> e)
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

        private void SavePeriodically()
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

        bool disposed = false;

        public void Dispose()
        {
            if (!this.disposed)
            {
                if (_webSocket.State == WebSocketState.Open || _webSocket.State == WebSocketState.Connecting)
                    _webSocket.Stop();
                _context.Dispose();
                this.disposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}
