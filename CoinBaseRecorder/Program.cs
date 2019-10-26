using CoinBaseRecorder.Constants;
using CoinBaseRecorder.Core.EventResponseModels;
using CoinBaseRecorder.Core.Services;
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
            using (ICoinBaseService service = new CoinBaseService(Authentication.ApiKey, Authentication.UnsignedSignature, Authentication.Passphrase, true)) // TODO: use any DI framework and inject services in constructors
            {
                service.OrderRecieved += OnOrderRecieved;
                Console.WriteLine("Reading history ...");
                service.PullHistoryAsync().Wait();
                Console.Clear();
                Console.WriteLine("Recording started...");
                service.StartRecording();
                Console.ReadLine();
            }
        }

        static void OnOrderRecieved(object sender, OrderRecievedEventArgs e)
        {
            Console.WriteLine(e.Time.ToString() + '\t' + e.ProductName + '\t' + e.Price);
        }
    }
}
