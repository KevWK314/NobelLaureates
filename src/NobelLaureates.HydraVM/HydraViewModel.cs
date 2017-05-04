using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NobelLaureates.HydraVM
{
    public abstract class HydraViewModel : PropertyChangedBase
    {
        private readonly ConcurrentDictionary<string, HydraViewModelProperty> _properties = new ConcurrentDictionary<string, HydraViewModelProperty>();

        protected HydraViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public object this[string name]
        {
            get
            {
                HydraViewModelProperty property;
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
            HydraViewModelProperty property;
            return _properties.TryGetValue(name, out property) ? property as HydraViewModelProperty<T> : null;
        }

        public IEnumerable<HydraViewModelProperty> GetAllProperties()
        {
            return _properties.Values.ToArray();
        }

        private void AddProperty<T>(HydraViewModelProperty<T> property)
        {
            _properties[property.Name] = property;
        }
    }
}
