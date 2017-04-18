using NobelLaureates.Ethereal;
using NobelLaureates.Ethereal.Messaging;
using NobelLaureates.HydraVM;
using NobelLaureates.Model;
using NobelLaureates.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace NobelLaureates.ViewModel.DataPanel
{
    public class LoadDataController : HydraController
    {
        private readonly IEther _ether;
        private readonly DataPanelViewModel _viewModel;
        private readonly List<NobelPrizeViewModel> _data = new List<NobelPrizeViewModel>();

        public LoadDataController(IEther ether, DataPanelViewModel viewModel)
        {
            _ether = ether;
            _viewModel = viewModel;
        }

        public async override void Start()
        {
            var data = await _ether.ExecuteAsync(NobelEther.Actions.NobelPrizeData, None.Default);
            Load(data);

            AddDisposable(_ether.Subscribe<string>(NobelEther.Messages.Search, s => Load(s)));
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

            var collection = _viewModel.Rows.Value;
            _data.ToList().ForEach(x => collection.Add(x));
        }

        private NobelPrizeViewModel CreateRow(NobelPrize item)
        {
            var row = new NobelPrizeViewModel();
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
