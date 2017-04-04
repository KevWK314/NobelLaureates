using System;

namespace NobelLaureates.HydraVM
{
    public static class HydraViewModelExtensions
    {
        public static HydraViewModelProperty<T> Initialise<T>(this HydraViewModelProperty<T> property, Action<HydraViewModelProperty<T>> initialise)
        {
            if(property != null && initialise != null)
            {
                initialise(property);
            }
            return property;
        }
    }
}
