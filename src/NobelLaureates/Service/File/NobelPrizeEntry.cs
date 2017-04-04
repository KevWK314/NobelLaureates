using FileHelpers;

namespace NobelLaureates.Service.File
{
    [DelimitedRecord(",")]
    [IgnoreFirst]
    public class NobelPrizeEntry
    {
        public int Year;

        public string Category;

        public string Prize;

        [FieldQuoted()]
        public string Motivation;

        public string PrizeShare;

        public string LaureateId;

        public string LaureateType;

        [FieldQuoted()]
        public string FullName;

        public string BirthDate;

        [FieldQuoted()]
        public string BirthCity;

        [FieldQuoted()]
        public string BirthCountry;

        public string Gender;

        [FieldQuoted()]
        public string OrganisationName;

        [FieldQuoted()]
        public string OrganisationCity;

        [FieldQuoted()]
        public string OrganisationCountry;

        public string DeathDate;

        [FieldQuoted()]
        public string DeathCity;

        [FieldQuoted()]
        public string DeathCountry;
    }
}
