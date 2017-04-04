using System;

namespace NobelLaureates.Model
{
    public class Laureate
    {
        public string LaureateId { get; set; }

        public string LaureateType { get; set; }

        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }

        public string BirthCity { get; set; }

        public string BirthCountry { get; set; }

        public DateTime DeathDate { get; set; }

        public string DeathCity { get; set; }

        public string DeathCountry { get; set; }

        public string Gender { get; set; }

        public string OrganisationName { get; set; }

        public string OrganisationCity { get; set; }

        public string OrganisationCountry { get; set; }
    }
}
