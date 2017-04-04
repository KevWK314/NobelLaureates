//using System.Linq;
//using System.Reactive.Subjects;

//using NobelLaureates.Ethereal;
//using NobelLaureates.Model;
//using AutoMapper;
//using System;
//using System.Reactive.Linq;
//using NobelLaureates.Core.Service.File;

//namespace NobelLaureates.Service.File
//{
//    public class NobelFileDataRegistrations : IEtherRegistrations
//    {
//        private const string Filename = "nobel.csv";

//        private readonly IMapper _mapper;
//        private readonly ICsvFileLoader _csvLoader;
//        private readonly IConnectableObservable<NobelPrize[]> _rawDataStream;

//        public NobelFileDataRegistrations(ICsvFileLoader csvLoader, IMapper mapper)
//        {
//            _mapper = mapper;
//            _csvLoader = csvLoader;
//            _rawDataStream = CreateLoadStream().Replay(1);
//        }

//        public void Register(IEther ether)
//        {
//            ether.RegisterAction(NobelEther.NobelPrizeDataAction,
//                r =>
//                    {
//                        _rawDataStream.Connect();
//                        return _rawDataStream;
//                    });
//        }

//        private IObservable<NobelPrize[]> CreateLoadStream()
//        {
//            return _csvLoader.LoadFile<NobelPrizeEntry>("nobel.csv").Select(Map);
//        }

//        private NobelPrize[] Map(NobelPrizeEntry[] data)
//        {
//            return data.Select(r => _mapper.Map<NobelPrize>(r)).ToArray();
//        }
//    }
//}
