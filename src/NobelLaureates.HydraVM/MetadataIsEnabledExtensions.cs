using System;

namespace NobelLaureates.HydraVM
{
    public static class MetadataIsEnabledExtensions
    {
        private static readonly string IsEnabled = "IsEnabled";

        public static HydraViewModelProperty<T> WithIsEnabled<T>(this HydraViewModelProperty<T> property, bool data)
        {
            property.AddMetaData<bool>(IsEnabled, data);
            return property;
        }

        public static HydraViewModelProperty<T> WithIsEnabled<T>(this HydraViewModelProperty<T> property)
        {
            property.AddMetaData<bool>(IsEnabled);
            return property;
        }

        public static bool GetIsEnabled<T>(this HydraViewModelProperty<T> property)
        {
            var meta = property.GetMetaData<bool>(IsEnabled);
            if(meta == null)
            {
                throw new InvalidOperationException("IsEnabled metadata not found");
            }
            return meta.Data;
        }

        public static void SetIsEnabled<T>(this HydraViewModelProperty<T> property, bool value)
        {
            var meta = property.GetMetaData<bool>(IsEnabled);
            if (meta == null)
            {
                throw new InvalidOperationException("IsEnabled metadata not found");
            }
            meta.Data = value;
        }
    }
}
