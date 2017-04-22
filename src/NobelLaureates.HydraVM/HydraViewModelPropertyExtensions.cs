using System;
using System.ComponentModel;

namespace NobelLaureates.HydraVM
{
    public static class HydraViewModelPropertyExtensions
    {
        public static IObservable<T> ValueStream<T>(this HydraViewModelProperty<T> viewModelProperty)
        {
            var valueStream = new HydraObservable<T>();
            PropertyChangedEventHandler update = (s, a) =>
            {
                if (a.PropertyName == nameof(HydraViewModelProperty<T>.Value))
                {
                    valueStream.OnNext(viewModelProperty.Value);
                }
            };
            viewModelProperty.PropertyChanged += update;
            valueStream.AddDisposable(new DisposableAction(() => viewModelProperty.PropertyChanged -= update));

            return valueStream;
        }


    }
}
