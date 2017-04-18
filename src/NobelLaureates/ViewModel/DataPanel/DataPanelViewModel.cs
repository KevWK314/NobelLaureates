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
            Rows = CreateProperty("Rows", new ObservableCollection<NobelPrizeViewModel>());
        }

        public HydraViewModelProperty<ObservableCollection<NobelPrizeViewModel>> Rows { get; private set; }
    }
}
