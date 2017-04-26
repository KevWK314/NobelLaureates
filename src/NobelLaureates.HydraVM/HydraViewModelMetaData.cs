using System;

namespace NobelLaureates.HydraVM
{
    public class HydraViewModelMetaData<T> : PropertyChangedBase
    {
        private T _data;

        internal HydraViewModelMetaData(string name)
        {
            Name = name;
        }

        internal HydraViewModelMetaData(string name, T data)
        {
            Name = name;
            Data = data;
        }

        public string Name{ get; private set; }

        public T Data
        {
            get { return _data; }
            set
            {
                SetField(ref _data, value);
            }
        }
    }
}
