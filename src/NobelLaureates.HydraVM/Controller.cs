using System;
using System.Reactive.Disposables;

namespace NobelLaureates.HydraVM
{
    public abstract class Controller : IDisposable
    {
        private CompositeDisposable _disposables = new CompositeDisposable();

        public abstract void Start();

        public void AddDisposable(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
