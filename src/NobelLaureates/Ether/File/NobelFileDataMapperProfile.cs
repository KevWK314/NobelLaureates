using System;
using AutoMapper;
using NobelLaureates.Model;
using System.Globalization;

namespace NobelLaureates.Ether.File
{
    public class NobelFileDataMapperProfile : Profile
    {
        public NobelFileDataMapperProfile()
        {
            CreateMap<NobelPrizeEntry, NobelPrize>()
                .ForMember(x => x.BirthDate, y => y.ResolveUsing(x => TryParseDate(x.BirthDate)))
                .ForMember(x => x.DeathDate, y => y.ResolveUsing(x => TryParseDate(x.DeathDate)));
        }

        private static DateTime? TryParseDate(string date)
        {
            DateTime res;
            return DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out res) ? res : (DateTime?)null;
        }
    }
}
