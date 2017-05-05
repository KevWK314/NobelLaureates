using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace NobelLaureates.HydraVM
{
    internal class HydraObservable<T> : IObservable<T>, IObserver<T>
    {
        private IObserver<T> _observer;
        private ConcurrentBag<IDisposable> _disposables = new ConcurrentBag<IDisposable>();

        public IDisposable Subscribe(IObserver<T> observer)
        {
            _observer = observer;

            return new DisposableAction(() => Dispose());
        }

        public void OnNext(T value)
        {
            _observer?.OnNext(value);
        }

        public void OnError(Exception error)
        {
            _observer?.OnError(error);
        }

        public void OnCompleted()
        {
            _observer?.OnCompleted();
        }

        public void AddDisposable(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        private void Dispose()
        {
            _observer = null;

            var disposables = _disposables;
            Interlocked.Exchange(ref _disposables, new ConcurrentBag<IDisposable>());
            disposables.ToList().ForEach(x => x.Dispose());
        }
    }
}
