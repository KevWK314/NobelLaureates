using System;
using System.Collections.Generic;

namespace NobelLaureates.HydraVM
{
    public class HydraViewModelProperty<T> : PropertyChangedBase
    {
        private Func<T> _valueGetter;
        private T _originalValue;
        private T _currentValue;
        private bool _hasChanges;

        private readonly Dictionary<string, object> _metaData = new Dictionary<string, object>();

        public event EventHandler<ValueChangedEventArgs<T>> ValueChanged;

        internal HydraViewModelProperty(string name)
            : this(name, () => default(T))
        {
            Name = name;
        }

        internal HydraViewModelProperty(string name, T defaultValue)
            : this(name, () => defaultValue)
        {
        }

        internal HydraViewModelProperty(string name, Func<T> defaultValueGetter)
        {
            Name = name;
            Reset(defaultValueGetter);
        }

        public string Name { get; private set; }

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
                    OnValueChanged(value);
                }
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

            OnValueChanged(_originalValue);
        }

        public ViewModelMetaData<TMeta> AddMetaData<TMeta>(string name)
        {
            return AddMetaData(name, default(TMeta));
        }

        public ViewModelMetaData<TMeta> AddMetaData<TMeta>(string name, TMeta data)
        {
            var metadata = new ViewModelMetaData<TMeta>(name, data);
            _metaData[name] = metadata;
            OnPropertyChanged(name);
            return metadata;
        }

        public bool HasMetaData<TMeta>(string name)
        {
            return GetMetaData<TMeta>(name) != null;
        }

        public ViewModelMetaData<TMeta> GetMetaData<TMeta>(string name)
        {
            object data;
            return _metaData.TryGetValue(name, out data) ? data as ViewModelMetaData<TMeta> : null;
        }

        public override string ToString()
        {
            return Value != null ? Value.ToString() : null;
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
            return Value == null ? 0 : Value.GetHashCode();
        }

        private void OnValueChanged(T newValue)
        {
            var handler = ValueChanged;
            handler?.Invoke(this, new ValueChangedEventArgs<T>(newValue));
        }
    }
}
