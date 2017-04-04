using NobelLaureates.HydraVM;
using System;
using System.Collections.ObjectModel;

namespace NobelLaureates.ViewModel.Grid
{
    public class GridViewModel : HydraViewModel
    {
        public GridViewModel()
            : base(typeof(GridViewModel).ToString())
        {
            Rows = CreateProperty("Rows", new ObservableCollection<GridRowViewModel>());
        }

        public HydraViewModelProperty<ObservableCollection<GridRowViewModel>> Rows { get; private set; }
    }
}
