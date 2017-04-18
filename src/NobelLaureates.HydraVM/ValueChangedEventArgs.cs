using System;

namespace NobelLaureates.HydraVM
{
    public class ValueChangedEventArgs<T> : EventArgs
    {
        public ValueChangedEventArgs(T newValue)
        {
            NewValue = newValue;
        }

        public T NewValue { get; private set; }
    }
}
