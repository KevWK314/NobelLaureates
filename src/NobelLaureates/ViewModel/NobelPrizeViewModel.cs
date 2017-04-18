using NobelLaureates.HydraVM;
using System;

namespace NobelLaureates.ViewModel
{
    public class NobelPrizeViewModel : HydraViewModel
    {
        public NobelPrizeViewModel()
            : base(typeof(NobelPrizeViewModel).ToString())
        {
            Year = CreateProperty<int>("Year");
            Category = CreateProperty<string>("Category");
            Prize = CreateProperty<string>("Prize");
            Motivation = CreateProperty<string>("Motivation");
            PrizeShare = CreateProperty<string>("PrizeShare");
            LaureateId = CreateProperty<string>("LaureateId");
            LaureateType = CreateProperty<string>("LaureateType");
            FullName = CreateProperty<string>("FullName");
            BirthDate = CreateProperty<DateTime?>("BirthDate");
            BirthCity = CreateProperty<string>("BirthCity");
            BirthCountry = CreateProperty<string>("BirthCountry");
            Gender = CreateProperty<string>("Gender");
            OrganisationName = CreateProperty<string>("OrganisationName");
            OrganisationCity = CreateProperty<string>("OrganisationCity");
            OrganisationCountry = CreateProperty<string>("OrganisationCountry");
            DeathDate = CreateProperty<DateTime?>("DeathDate");
            DeathCity = CreateProperty<string>("DeathCity");
            DeathCountry = CreateProperty<string>("DeathCountry");
        }

        public HydraViewModelProperty<int> Year { get; private set; }

        public HydraViewModelProperty<string> Category { get; private set; }

        public HydraViewModelProperty<string> Prize { get; private set; }

        public HydraViewModelProperty<string> Motivation { get; private set; }

        public HydraViewModelProperty<string> PrizeShare { get; private set; }

        public HydraViewModelProperty<string> LaureateId { get; private set; }

        public HydraViewModelProperty<string> LaureateType { get; private set; }

        public HydraViewModelProperty<string> FullName { get; private set; }

        public HydraViewModelProperty<DateTime?> BirthDate { get; private set; }

        public HydraViewModelProperty<string> BirthCity { get; private set; }

        public HydraViewModelProperty<string> BirthCountry { get; private set; }

        public HydraViewModelProperty<string> Gender { get; private set; }

        public HydraViewModelProperty<string> OrganisationName { get; private set; }

        public HydraViewModelProperty<string> OrganisationCity { get; private set; }

        public HydraViewModelProperty<string> OrganisationCountry { get; private set; }

        public HydraViewModelProperty<DateTime?> DeathDate { get; private set; }

        public HydraViewModelProperty<string> DeathCity { get; private set; }

        public HydraViewModelProperty<string> DeathCountry { get; private set; }

        public override string ToString()
        {
            return $"{Year} - {Category} - {FullName}";
        }
    }
}
