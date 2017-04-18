using NobelLaureates.HydraVM;

namespace NobelLaureates.ViewModel.SearchPanel
{
    public class SearchPanelControllers : HydraController
    {
        private readonly SearchController _search;

        public SearchPanelControllers(SearchController search)
        {
            _search = search;
        }

        public override void Start()
        {
            _search.Start();
        }
    }
}
