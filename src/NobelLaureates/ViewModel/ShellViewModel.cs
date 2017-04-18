using System;
using System.Collections.Generic;
using System.Linq;
using NobelLaureates.HydraVM;
using NobelLaureates.ViewModel.DataPanel;
using NobelLaureates.ViewModel.SearchPanel;

namespace NobelLaureates.ViewModel
{
    public class ShellViewModel : HydraViewModelBag
    {
        public ShellViewModel(DataPanelViewModel dataPanelViewModel, SearchPanelViewModel searchPanelViewModel)
        {
            DataPanelViewModel = dataPanelViewModel;
            SearchPanelViewModel = searchPanelViewModel;

            AddViewModel(DataPanelViewModel);
            AddViewModel(SearchPanelViewModel);
        }

        public DataPanelViewModel DataPanelViewModel { get; private set; }

        public SearchPanelViewModel SearchPanelViewModel { get; private set; }
    }
}
