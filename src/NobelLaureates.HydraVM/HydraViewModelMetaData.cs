using System;

namespace NobelLaureates.HydraVM
{
    public class ViewModelMetaData<T> : PropertyChangedBase
    {
        private T _data;

        internal ViewModelMetaData(string name)
        {
            Name = name;
        }

        internal ViewModelMetaData(string name, T data)
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
