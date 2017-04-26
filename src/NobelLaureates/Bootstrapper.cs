using System.Linq;
using Ninject.Modules;
using Ninject;
using NobelLaureates.Ether;
using NobelLaureates.Ether.File;
using NobelLaureates.Ethereal;
using AutoMapper;
using NobelLaureates.ViewModel.DataPanel;
using NobelLaureates.Core.Service.File;
using NobelLaureates.ViewModel.SearchPanel;
using NobelLaureates.ViewModel;

namespace NobelLaureates
{
    public class Bootstrapper : NinjectModule
    {
        private StandardKernel _kernel;
        private NobelPrizeContainer _rootContainer;

        public Bootstrapper()
        {
            _kernel = new StandardKernel(this);
            Start();
        }

        public NobelPrizeContainer RootContainer
        {
            get { return _rootContainer; }
        }

        public override void Load()
        {
            Bind<ICsvFileLoader>().To<CsvFileLoader>().InSingletonScope();

            var mapperConfig = new MapperConfiguration(
                 cfg =>
                 {
                     cfg.AddProfile<NobelFileDataMapperProfile>();
                 });
            var mapper = mapperConfig.CreateMapper();
            Bind<IMapper>().ToConstant(mapper);

            Bind<IEther>().To<Ethereal.Ether>().InSingletonScope();
            Bind<IEtherRegistrations>().To<NobelEther>().InSingletonScope();

            Bind<NobelPrizeContainer>().ToSelf().InSingletonScope();
        }

        public void Start()
        {
            // Register Ether
            var ether = _kernel.Get<IEther>();
            var regs = _kernel.GetAll<IEtherRegistrations>();
            regs.ToList().ForEach(r => r.Register(ether));

            // Generate ViewModels and start container
            _rootContainer = _kernel.Get<NobelPrizeContainer>();
            _rootContainer
                .AddBehaviour(_kernel.Get<LoadDataController>())
                .AddBehaviour(_kernel.Get<SearchController>());
            _rootContainer.Start();
        }
    }
}
