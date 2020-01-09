using System;
using System.Collections.Generic;

namespace CustomerMgmt.Models
{
    public partial class CountryList
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string Iso3 { get; set; }
        public string Iso2 { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
