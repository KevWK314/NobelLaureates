using NobelLaureates.HydraVM;
using NobelLaureates.ViewModel.DataPanel;
using NobelLaureates.ViewModel.SearchPanel;

namespace NobelLaureates.ViewModel
{
    public class NobelPrizeContainer : HydraViewModelContainer
    {
        public NobelPrizeContainer()
        {
            DataPanelViewModel = new DataPanelViewModel();
            SearchPanelViewModel = new SearchPanelViewModel();
        }

        public DataPanelViewModel DataPanelViewModel
        {
            get; private set;
        }

        public SearchPanelViewModel SearchPanelViewModel
        {
            get; private set;
        }
    }
}
