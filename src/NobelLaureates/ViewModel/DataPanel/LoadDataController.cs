using NobelLaureates.Ethereal;
using NobelLaureates.Ethereal.Messaging;
using NobelLaureates.HydraVM;
using NobelLaureates.Model;
using NobelLaureates.Ether;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace NobelLaureates.ViewModel.DataPanel
{
    public class LoadDataController : IHydraBehaviour
    {
        private readonly IEther _ether;
        private readonly NobelPrizeContainer _component;
        private readonly List<NobelPrizeRowViewModel> _data = new List<NobelPrizeRowViewModel>();
        private readonly SerialDisposable _disposable = new SerialDisposable();

        public LoadDataController(IEther ether, NobelPrizeContainer component)
        {
            _ether = ether;
            _component = component;
        }

        public async void Start()
        {
            var data = await _ether.ExecuteAsync(NobelEther.Actions.NobelPrizeData, None.Default);
            Load(data);

            _disposable.Disposable = _ether.Subscribe<string>(NobelEther.Messages.Search, s => Load(s));
        }

        public void Stop()
        {
            _disposable.Disposable = null;
        }

        private void Load(NobelPrize[] data)
        {
            _data.Clear();
            _data.AddRange(data.Select(CreateRow));

            Load(string.Empty);
        }

        private void Load(string searchString)
        {
            // TBD. Search

            var collection = _component.DataPanelViewModel.Rows.Value;
            collection.Clear();

            var filtered = string.IsNullOrEmpty(searchString) ?
                _data :
                _data.Where(x => x.Category.Value.IndexOf(searchString, StringComparison.InvariantCultureIgnoreCase) > -1).ToList();
            filtered.ForEach(x => collection.Add(x));
        }

        private NobelPrizeRowViewModel CreateRow(NobelPrize item)
        {
            var row = new NobelPrizeRowViewModel();
            row.Year.Reset(item.Year);
            row.Category.Reset(item.Category);
            row.Prize.Reset(item.Prize);
            row.Motivation.Reset(item.Motivation);
            row.PrizeShare.Reset(item.PrizeShare);
            row.LaureateId.Reset(item.LaureateId);
            row.LaureateType.Reset(item.LaureateType);
            row.FullName.Reset(item.FullName);
            row.BirthDate.Reset(item.BirthDate);
            row.BirthCity.Reset(item.BirthCity);
            row.BirthCountry.Reset(item.BirthCountry);
            row.Gender.Reset(item.Gender);
            row.OrganisationName.Reset(item.OrganisationName);
            row.OrganisationCity.Reset(item.OrganisationCity);
            row.OrganisationCountry.Reset(item.OrganisationCountry);
            row.DeathDate.Reset(item.DeathDate);
            row.DeathCity.Reset(item.DeathCity);
            row.DeathCountry.Reset(item.DeathCountry);

            return row;
        }
    }
}
