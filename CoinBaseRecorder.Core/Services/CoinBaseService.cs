using CoinbasePro;
using CoinbasePro.Exceptions;
using CoinbasePro.Network.Authentication;
using CoinbasePro.Services.Products.Types;
using CoinbasePro.Shared.Types;
using CoinbasePro.WebSocket.Models.Response;
using CoinbasePro.WebSocket.Types;
using CoinBaseRecorder.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocket4Net;

namespace CoinBaseRecorder.Core.Services
{
    public class CoinBaseService : ICoinBaseService
    {
        private readonly CoinBaseContext _context;
        private readonly CoinbaseProClient _client;
        public CoinBaseService(string apiKey, string unsignedSignature, string passphrase, bool useSendBox = false)
        {
            _context = new CoinBaseContext();
            var authenticator = new Authenticator(apiKey, unsignedSignature, passphrase);
            _client = new CoinbaseProClient(authenticator, true);
            //_client = new CoinbaseProClient();
            _client.WebSocket.OnTickerReceived += WebSocket_OnTickerReceived;
        }

        public void StartRecording()
        {
            var productTypes = Enum.GetValues(typeof(ProductType)).Cast<ProductType>().ToList();
            var channels = new List<ChannelType>() { ChannelType.Ticker };
            _client.WebSocket.Start(productTypes, channels);
            SavePeriodically();
        }

        public async Task PullHistoryAsync()
        {
            _context.HistoricalPriceChanges.RemoveRange(_context.HistoricalPriceChanges);
            var productTypes = Enum.GetValues(typeof(ProductType)).Cast<ProductType>().ToList();
            //productTypes.Remove(ProductType.Unknown);
            foreach (var prodType in productTypes)
            {
                //try
                //{
                var result = await _client.ProductsService.GetHistoricRatesAsync(prodType, DateTime.MinValue, DateTime.Today.AddDays(1), CandleGranularity.Hour24);

                var list = result.Select(x => new HistoricalPriceChange
                {
                    Id = Guid.NewGuid(),
                    Price_Close = x.Close,
                    Price_High = x.High,
                    Price_Low = x.Low,
                    Price_Open = x.Open,
                    ProdId = (int)prodType,
                    Time = x.Time,
                    Volume = x.Volume
                });
                _context.HistoricalPriceChanges.AddRange(list);
                //}
                //catch (AggregateException ex)
                //{
                //    if (ex.InnerException.GetType() == typeof(CoinbaseProHttpException) && ex.InnerException.Message == "NotFound")
                //        continue;
                //    else
                //        throw;
                //}

            }
            await _context.SaveChangesAsync();
        }

        private void WebSocket_OnTickerReceived(object sender, WebfeedEventArgs<Ticker> e)
        {
            var priceChangeObj = new PriceChange
            {
                Id = Guid.NewGuid(),
                TradeId = e.LastOrder.TradeId,
                ProdId = (int)e.LastOrder.ProductId,
                Price = e.LastOrder.Price,
                Time = e.LastOrder.Time
            };
            _context.PriceChanges.Add(priceChangeObj);
            Console.WriteLine(priceChangeObj.Time.ToString() + '\t' + e.LastOrder.ProductId + '\t' + priceChangeObj.Price);
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

        #region IDisposable

        private bool disposed = false;

        public void Dispose()
        {
            if (!disposed)
            {
                if (_client.WebSocket.State == WebSocketState.Open)
                    _client.WebSocket.Stop();
                _context.Dispose();
                disposed = true;
            }
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
