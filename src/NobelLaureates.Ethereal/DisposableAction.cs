using System;

namespace NobelLaureates.Ethereal
{
    internal class DisposableAction : IDisposable
    {
        private bool _isDisposed = false;
        private Action _disposableAction;

        public DisposableAction(Action disposableAction)
        {
            if (disposableAction == null) throw new ArgumentNullException(nameof(disposableAction));
            _disposableAction = disposableAction;
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                _disposableAction();
            }
        }
    }
}
