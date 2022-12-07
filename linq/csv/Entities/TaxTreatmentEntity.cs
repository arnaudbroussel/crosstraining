using System;

namespace crosstraining.linq.csv.Entities {
    public class TaxTreatmentEntity {
        public Guid Id { get; set; }
        public string Legislation { get; set; }
        public string TaxTreatment { get; set; }
        public string RegionForTaxes { get; set; }
        public bool UseFromTaxes { get; set; }
        public Guid? RegionForTaxesId { get; set; }
    }
}