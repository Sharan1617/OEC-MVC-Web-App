using System;
using System.Collections.Generic;

namespace OEC.Models
{
    public partial class Province
    {
        public Province()
        {
            Farm = new HashSet<Farm>();
        }

        public string ProvinceCode { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string RetailTaxName { get; set; }
        public double? RetailTaxRate { get; set; }
        public bool? FederalTaxIncluded { get; set; }

        public virtual ICollection<Farm> Farm { get; set; }
    }
}
