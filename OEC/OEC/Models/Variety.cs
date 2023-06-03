using System;
using System.Collections.Generic;

namespace OEC.Models
{
    public partial class Variety
    {
        public Variety()
        {
            Plot = new HashSet<Plot>();
        }

        public int VarietyId { get; set; }
        public int? CropId { get; set; }
        public string Name { get; set; }

        public virtual Crop Crop { get; set; }
        public virtual ICollection<Plot> Plot { get; set; }
    }
}
