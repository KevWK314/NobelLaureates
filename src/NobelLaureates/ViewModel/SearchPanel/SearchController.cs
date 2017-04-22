using NobelLaureates.Ethereal;
using NobelLaureates.Ethereal.Messaging;
using NobelLaureates.HydraVM;
using NobelLaureates.Service;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace NobelLaureates.ViewModel.SearchPanel
{
    public class SearchController : HydraController
    {
        private readonly IEther _ether;
        private readonly SearchPanelViewModel _viewModel;

        public SearchController(IEther ether, SearchPanelViewModel viewModel)
        {
            _ether = ether;
            _viewModel = viewModel;
        }

        public override void Start()
        {
            AddDisposable(_viewModel.SearchText.ValueStream()
                    .Throttle(TimeSpan.FromSeconds(0.5))
                    .DistinctUntilChanged()
                    .ObserveOn(DispatcherScheduler.Current.Dispatcher)
                    .Subscribe(Search));
        }

        private void Search(string text)
        {
            _ether.Publish(text, NobelEther.Messages.Search);
        }
    }
}
