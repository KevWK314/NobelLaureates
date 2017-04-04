using System;
using System.Collections.Concurrent;

namespace NobelLaureates.HydraVM
{
    public abstract class HydraViewModel : PropertyChangedBase
    {
        private readonly ConcurrentDictionary<string, object> _properties = new ConcurrentDictionary<string, object>();

        protected HydraViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public object this[string name]
        {
            get
            {
                object property;
                return _properties.TryGetValue(name, out property) ? property : null;
            }
        }

        public HydraViewModelProperty<T> CreateProperty<T>(string name)
        {
            var property = new HydraViewModelProperty<T>(name);
            AddProperty(property);
            return property;
        }

        public HydraViewModelProperty<T> CreateProperty<T>(string name, T defaultValue)
        {
            var property = new HydraViewModelProperty<T>(name, defaultValue);
            AddProperty(property);
            return property;
        }

        public HydraViewModelProperty<T> CreateProperty<T>(string name, Func<T> defaultValueGetter)
        {
            var property = new HydraViewModelProperty<T>(name, defaultValueGetter);
            AddProperty(property);
            return property;
        }

        public HydraViewModelProperty<T> GetProperty<T>(string name)
        {
            object property;
            return _properties.TryGetValue(name, out property) ? property as HydraViewModelProperty<T> : null;
        }

        private void AddProperty<T>(HydraViewModelProperty<T> property)
        {
            _properties[property.Name] = property;
        }
    }
}
