using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace CDPBridges
{
    public interface IBridge : IDisposable
    {
        void Start();
        
        UniTask<SendResult> SendEventAsync(CDPEvent cdpEvent, CancellationToken token);
    }
}