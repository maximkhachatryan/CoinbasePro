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
            using (ICoinBaseService repo = new CoinBaseService()) // TODO: use any DI framework
            {
                repo.PullHistoryAsync();
                repo.StartRecording();
                Console.ReadLine();
            }
        }
    }
}
