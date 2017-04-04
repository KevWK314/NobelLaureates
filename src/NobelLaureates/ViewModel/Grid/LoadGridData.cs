using NobelLaureates.Ethereal;
using NobelLaureates.HydraVM;
using NobelLaureates.Model;
using NobelLaureates.Service;
using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace NobelLaureates.ViewModel.Grid
{
    public class LoadGridData : HydraController
    {
        private readonly IEther _ether;
        private readonly GridViewModel _viewModel;

        public LoadGridData(IEther ether, GridViewModel viewModel)
        {
            _ether = ether;
            _viewModel = viewModel;
        }

        public async override void Start()
        {
            var data = await _ether.ExecuteAsync(NobelEther.Actions.NobelPrizeData, None.Default);
            Load(data);
        }

        private void Load(NobelPrize[] data)
        {
            var collection = _viewModel.Rows.Value;
            data.ToList().ForEach(x => collection.Add(CreateRow(x)));
        }

        private GridRowViewModel CreateRow(NobelPrize item)
        {
            var row = new GridRowViewModel();
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
