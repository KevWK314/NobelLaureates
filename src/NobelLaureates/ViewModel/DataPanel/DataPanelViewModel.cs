using NobelLaureates.HydraVM;
using System;
using System.Collections.ObjectModel;

namespace NobelLaureates.ViewModel.DataPanel
{
    public class DataPanelViewModel : HydraViewModel
    {
        public DataPanelViewModel()
            : base(typeof(DataPanelViewModel).ToString())
        {
            Rows = CreateProperty("Rows", new ObservableCollection<NobelPrizeRowViewModel>());
        }

        public HydraViewModelProperty<ObservableCollection<NobelPrizeRowViewModel>> Rows { get; private set; }
    }
}
