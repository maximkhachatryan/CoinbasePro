using System;

namespace CoinBaseRecorder
{
    class Program
    {
        private static void Main(string[] args)
        {
            using (var recorder = new CoinBaseRecorder())
            {
                recorder.StartRecording();
                Console.ReadLine();
            }
        }

    }
}
