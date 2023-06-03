using System;
using System.Collections.Generic;

namespace OEC.Models
{
    public partial class Treatment
    {
        public Treatment()
        {
            TreatmentFertilizer = new HashSet<TreatmentFertilizer>();
        }

        public int TreatmentId { get; set; }
        public string Name { get; set; }
        public int PlotId { get; set; }
        public float? Moisture { get; set; }
        public double? Yield { get; set; }
        public double? Weight { get; set; }

        public virtual Plot Plot { get; set; }
        public virtual ICollection<TreatmentFertilizer> TreatmentFertilizer { get; set; }
    }
}
