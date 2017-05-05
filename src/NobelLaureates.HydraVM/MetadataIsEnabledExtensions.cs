namespace NobelLaureates.HydraVM
{
    /// <summary>
    /// This is just an example of a re-usable approach to metadata
    /// </summary>
    public static class MetadataIsEnabledExtensions
    {
        private static readonly MetaDataKey<bool> IsEnabled = new MetaDataKey<bool>("IsEnabled");

        public static HydraViewModelProperty<T> WithIsEnabled<T>(this HydraViewModelProperty<T> property)
        {
            property.SetMetaData(IsEnabled);
            return property;
        }

        public static HydraViewModelProperty<T> WithIsEnabled<T>(this HydraViewModelProperty<T> property, bool data)
        {
            property.SetMetaData(IsEnabled, data);
            return property;
        }

        public static bool GetIsEnabled<T>(this HydraViewModelProperty<T> property)
        {
            return property.GetMetaData(IsEnabled);
        }

        public static void SetIsEnabled<T>(this HydraViewModelProperty<T> property, bool value)
        {
            property.SetMetaData(IsEnabled, value);
        }
    }
}
