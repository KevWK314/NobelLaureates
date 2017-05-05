using System;
using System.ComponentModel;

namespace NobelLaureates.HydraVM
{
    public static class HydraViewModelPropertyExtensions
    {
        public static IObservable<T> ValueStream<T>(this HydraViewModelProperty<T> viewModelProperty)
        {
            if (viewModelProperty == null) throw new ArgumentNullException(nameof(viewModelProperty));

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

        public static HydraViewModelProperty<T> WithValueFormatter<T>(this HydraViewModelProperty<T> viewModelProperty, Func<T, string> valueFormatter)
        {
            if (viewModelProperty == null) throw new ArgumentNullException(nameof(viewModelProperty));
            if (valueFormatter == null) throw new ArgumentNullException(nameof(valueFormatter));

            viewModelProperty.FormatValue(valueFormatter);

            return viewModelProperty;
        }
    }
}
