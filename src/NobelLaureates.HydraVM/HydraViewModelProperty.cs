using System;
using System.Collections.Generic;

namespace NobelLaureates.HydraVM
{
    public abstract class HydraViewModelProperty : PropertyChangedBase
    {
        protected internal HydraViewModelProperty(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public abstract string FormattedValue { get; }
    }

    public class HydraViewModelProperty<T> : HydraViewModelProperty
    {
        private Func<T> _valueGetter;
        private T _originalValue;
        private T _currentValue;
        private bool _hasChanges;
        private Func<T, string> _valueFormatter =
            value => value != null ? value.ToString() : null;

        private readonly Dictionary<string, object> _metaData = new Dictionary<string, object>();

        internal HydraViewModelProperty(string name)
            : this(name, () => default(T))
        {
        }

        internal HydraViewModelProperty(string name, T defaultValue)
            : this(name, () => defaultValue)
        {
        }

        internal HydraViewModelProperty(string name, Func<T> defaultValueGetter)
            : base(name)
        {
            Reset(defaultValueGetter);
        }

        public bool HasChanges
        {
            get { return _hasChanges; }
            private set
            {
                SetField(ref _hasChanges, value);
            }
        }

        public T Value
        {
            get { return _currentValue; }
            set
            {
                var oldValue = _currentValue;
                if (SetField(ref _currentValue, value))
                {
                    HasChanges = !EqualityComparer<T>.Default.Equals(_originalValue, value);
                }
            }
        }

        public override string FormattedValue
        {
            get { return _valueFormatter(Value); }
        }

        public object this[string metaDataName]
        {
            get
            {
                object metaData;
                return _metaData.TryGetValue(metaDataName, out metaData) ? metaData : null;
            }
        }

        public void Reset()
        {
            Reset(_valueGetter);
        }

        public void Reset(T defaultValue)
        {
            Reset(() => defaultValue);
        }

        public void Reset(Func<T> defaultValueGetter)
        {
            _valueGetter = defaultValueGetter ?? (() => default(T));
            _originalValue = _valueGetter();

            SetField(ref _currentValue, _originalValue, this.PropertyName(x => x.Value));
            SetField(ref _hasChanges, false, this.PropertyName(x => x.HasChanges));
        }

        internal void FormatValue(Func<T, string> valueFormatter)
        {
            if (valueFormatter == null) throw new ArgumentNullException(nameof(valueFormatter));

            _valueFormatter = valueFormatter;
        }

        public HydraViewModelMetaData<TMeta> AddMetaData<TMeta>(string name)
        {
            return AddMetaData(name, default(TMeta));
        }

        public HydraViewModelMetaData<TMeta> AddMetaData<TMeta>(string name, TMeta data)
        {
            var metadata = new HydraViewModelMetaData<TMeta>(name, data);
            _metaData[name] = metadata;
            OnPropertyChanged($"Item[name]");
            return metadata;
        }

        public bool HasMetaData<TMeta>(string name)
        {
            return GetMetaData<TMeta>(name) != null;
        }

        public HydraViewModelMetaData<TMeta> GetMetaData<TMeta>(string name)
        {
            object data;
            return _metaData.TryGetValue(name, out data) ? data as HydraViewModelMetaData<TMeta> : null;
        }

        public override string ToString()
        {
            return FormattedValue;
        }

        public override bool Equals(object obj)
        {
            var other = obj as HydraViewModelProperty<T>;
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            return Value == null ? 0 : Name.GetHashCode() & Value.GetHashCode();
        }
    }
}
