using NobelLaureates.HydraVM;

namespace NobelLaureates.ViewModel.SearchPanel
{
    public class SearchPanelViewModel : HydraViewModel
    {
        public SearchPanelViewModel()
            : base(typeof(SearchPanelViewModel).ToString())
        {
            SearchText = CreateProperty(nameof(SearchText), string.Empty);        
        }

        public HydraViewModelProperty<string> SearchText { get; private set; }
    }
}
