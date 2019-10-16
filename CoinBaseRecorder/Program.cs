using CoinbasePro.Shared.Types;
using CoinbasePro.WebSocket.Models.Response;
using CoinbasePro.WebSocket.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBaseRecorder
{
    class Program
    {
        static void Main(string[] args)
        {
            var coinbaseProClient = new CoinbasePro.CoinbaseProClient();

            //use the websocket feed
            var productTypes = Enum.GetValues(typeof(ProductType)).Cast<ProductType>().ToList();
            var channels = new List<ChannelType>() { ChannelType.Ticker }; // When not providing any channels, the socket will subscribe to all channels

            var webSocket = coinbaseProClient.WebSocket;
            webSocket.Start(productTypes, channels);

            // EventHandler for the heartbeat response type
            webSocket.OnTickerReceived += WebSocket_OnTickerReceived;

            Console.ReadKey();
            webSocket.Stop();
        }

        static void WebSocket_OnTickerReceived(object sender, WebfeedEventArgs<Ticker> e)
        {
            Console.WriteLine(e.LastOrder.Time.ToString() + '\t' + e.LastOrder.ProductId + '\t' + e.LastOrder.Price);
        }
    }
}
