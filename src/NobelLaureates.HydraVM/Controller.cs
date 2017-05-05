using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace NobelLaureates.HydraVM
{
    public abstract class Controller : IDisposable
    {
        private ConcurrentBag<IDisposable> _disposables = new ConcurrentBag<IDisposable>();

        public abstract void Start();

        public void AddDisposable(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public void Dispose()
        {
            var disposables = _disposables;
            Interlocked.Exchange(ref _disposables, new ConcurrentBag<IDisposable>());
            disposables.ToList().ForEach(x => x.Dispose());
        }
    }
}
