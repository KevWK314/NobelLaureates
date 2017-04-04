using AutoMapper;
using NobelLaureates.Core.Service.File;
using NobelLaureates.Ethereal;
using NobelLaureates.Model;
using NobelLaureates.Service.File;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace NobelLaureates.Service
{
    public class NobelEther : IEtherRegistrations
    {
        public static class Services
        {
            public static EtherService<ICsvFileLoader> CsvFileLoaderService = EtherService.Create<ICsvFileLoader>(nameof(CsvFileLoaderService));
        }

        public static class Actions
        {
            public static EtherAction<None, NobelPrize[]> NobelPrizeData = EtherAction.Create<None, NobelPrize[]>(nameof(NobelPrizeData));
        }

        private readonly IMapper _mapper;
        private readonly ICsvFileLoader _csvFileLoader;

        public NobelEther(IMapper mapper, ICsvFileLoader csvFileLoader)
        {
            _mapper = mapper;
            _csvFileLoader = csvFileLoader;
        }

        public void Register(IEther ether)
        {
            ether.RegisterService(Services.CsvFileLoaderService, _csvFileLoader);

            ether.WithService(Services.CsvFileLoaderService)
                .RegisterAction(Actions.NobelPrizeData, (s, r) => Map(s.LoadFile<NobelPrizeEntry>("nobel.csv")))
                .AddListener(x => x.WhenError((ea, ex) => Console.WriteLine("Failed to execute {0]", ea.Name)));
        }

        private NobelPrize[] Map(NobelPrizeEntry[] data)
        {
            return data.Select(r => _mapper.Map<NobelPrize>(r)).ToArray();
        }
    }
}
