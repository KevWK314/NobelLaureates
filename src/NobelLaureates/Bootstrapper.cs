﻿using System;
using System.Linq;
using Ninject.Modules;
using Ninject;
using NobelLaureates.Service;
using NobelLaureates.Service.File;
using NobelLaureates.Ethereal;
using AutoMapper;
using NobelLaureates.Model;
using NobelLaureates.ViewModel;
using NobelLaureates.HydraVM;
using NobelLaureates.ViewModel.Grid;
using NobelLaureates.Core.Service.File;

namespace NobelLaureates
{
    public class Bootstrapper : NinjectModule
    {
        private StandardKernel _kernel;
        private HydraViewModelBag _shell;

        public Bootstrapper()
        {
            _kernel = new StandardKernel(this);
            Start();
        }

        public HydraViewModelBag Shell
        {
            get { return _shell; }
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

            Bind<IEther>().To<Ether>().InSingletonScope();
            Bind<IEtherRegistrations>().To<NobelEther>().InSingletonScope();

            Bind<ShellViewModel>().ToSelf().InSingletonScope();

            Bind<GridViewModel>().ToSelf().InSingletonScope();
            Bind<GridViewModelControllers>().ToSelf().InSingletonScope();

        }

        public void Start()
        {
            // Register
            var ether = _kernel.Get<IEther>();
            var regs = _kernel.GetAll<IEtherRegistrations>();
            regs.ToList().ForEach(r => r.Register(ether));

            // Generate ViewModel
            _shell = _kernel.Get<ShellViewModel>();

            // Start Controllers
            _kernel.Get<GridViewModelControllers>().Start();
        }
    }
}