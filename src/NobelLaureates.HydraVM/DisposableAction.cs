using System;
using System.Threading;

namespace NobelLaureates.HydraVM
{
    internal class DisposableAction : IDisposable
    {
        private int _isDisposed = 0;
        private Action _disposableAction;

        public DisposableAction(Action disposableAction)
        {
            if (disposableAction == null) throw new ArgumentNullException(nameof(disposableAction));
            _disposableAction = disposableAction;
        }

        public void Dispose()
        {
            if (Interlocked.CompareExchange(ref _isDisposed, 1, 0) == 0)
            {
                _disposableAction();
            }
        }
    }
}
