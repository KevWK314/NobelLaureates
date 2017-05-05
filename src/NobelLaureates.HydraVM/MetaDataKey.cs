using System;

namespace NobelLaureates.HydraVM
{
    public class MetaDataKey
    {
        public MetaDataKey(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException(nameof(name));

            Name = name;
        }

        public string Name
        {
            get; private set;
        }
    }

    public sealed class MetaDataKey<T> : MetaDataKey
    {
        public MetaDataKey(string name)
            : base(name)
        {
        }
    }
}
