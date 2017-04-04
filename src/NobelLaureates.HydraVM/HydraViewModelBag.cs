using System.Collections.Generic;

namespace NobelLaureates.HydraVM
{
    public class HydraViewModelBag
    {
        private Dictionary<string, HydraViewModel> _entries = new Dictionary<string, HydraViewModel>();

        public HydraViewModel this[string key]
        {
            get { return GetItem(key); }
        }

        public void AddViewModel(HydraViewModel viewModel)
        {
            _entries[viewModel.Name] = viewModel;
        }

        public HydraViewModel GetItem(string key)
        {
            HydraViewModel entry;
            _entries.TryGetValue(key, out entry);
            return entry;
        }
    }
}
