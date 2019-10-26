using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinBaseRecorder.Core.Services
{
    public interface ICoinBaseService: IDisposable
    {
        void StartRecording();
        Task PullHistoryAsync();
    }
}
