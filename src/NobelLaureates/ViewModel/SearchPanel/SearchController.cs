using NobelLaureates.Ethereal;
using NobelLaureates.Ethereal.Messaging;
using NobelLaureates.HydraVM;
using NobelLaureates.Ether;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace NobelLaureates.ViewModel.SearchPanel
{
    public class SearchController : IHydraBehaviour
    {
        private readonly IEther _ether;
        private readonly NobelPrizeContainer _component;
        private readonly SerialDisposable _disposable = new SerialDisposable()
;
        public SearchController(IEther ether, NobelPrizeContainer component)
        {
            _ether = ether;
            _component = component;
        }

        public void Start()
        {
            _disposable.Disposable = _component.SearchPanelViewModel.SearchText.ValueStream()
                    .Throttle(TimeSpan.FromSeconds(0.5))
                    .DistinctUntilChanged()
                    .ObserveOn(DispatcherScheduler.Current.Dispatcher)
                    .Subscribe(Search);
        }

        private void Search(string text)
        {
            _ether.Publish(text, NobelEther.Messages.Search);
        }

        public void Stop()
        {
            _disposable.Disposable = null;
        }
    }
}
