using CoinBaseRecorder.Core.EventResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBaseRecorder.Core.Services
{
    public interface ICoinBaseService : IDisposable
    {
        event EventHandler<OrderRecievedEventArgs> OrderRecieved;
        void StartRecording();
        Task PullHistoryAsync();
        
    }
}
