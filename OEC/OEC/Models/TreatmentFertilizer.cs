using System;
using System.Collections.Generic;

namespace OEC.Models
{
    public partial class TreatmentFertilizer
    {
        public int TreatmentFertilizerId { get; set; }
        public int? TreatmentId { get; set; }
        public string FertilizerName { get; set; }
        public double? RatePerAcre { get; set; }
        public string RateMetric { get; set; }

        public virtual Fertilizer FertilizerNameNavigation { get; set; }
        public virtual Treatment Treatment { get; set; }
    }
}
