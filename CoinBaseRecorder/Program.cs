using CoinBaseRecorder.Constants;
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
            using (ICoinBaseService service = new CoinBaseService(Authentication.ApiKey, Authentication.UnsignedSignature, Authentication.Passphrase)) // TODO: use any DI framework and inject services in constructors
            {
                service.PullHistoryAsync().Wait();
                service.StartRecording();
                Console.ReadLine();
            }
        }
    }
}
