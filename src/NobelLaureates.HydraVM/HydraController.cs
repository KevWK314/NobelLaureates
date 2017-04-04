using System;
using System.Reactive.Disposables;

namespace NobelLaureates.HydraVM
{
    public abstract class HydraController : IDisposable
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public void AddDisposable(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public abstract void Start();

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
