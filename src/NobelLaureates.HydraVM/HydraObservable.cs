using System;
using System.Collections.Generic;
using System.Linq;

namespace NobelLaureates.HydraVM
{
    internal class HydraObservable<T> : IObservable<T>, IObserver<T>
    {
        private IObserver<T> _observer;
        private List<IDisposable> _disposables = new List<IDisposable>();

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

            var disposables = _disposables.ToList();
            _disposables.Clear();
            disposables.ForEach(x => x.Dispose());
        }
    }
}
