using System;

namespace crosstraining.linq.csv.Entities {
    public class TaxRateEntity {
        public Guid Id { get; set; }
        public string Legislation { get; set; }
        public string Name { get; set; }
        public string RegionForTaxes { get; set; }
        public string TaxTreatment { get; set; }
        public string TaxItemType { get; set; }
        public string TaxCode { get; set; }
        public double Rate { get; set; }
        public Guid? RegionForTaxesId { get; set; }
        public Guid? TaxTreatmentId { get; set; }
        public Guid? TaxItemTypeId { get; set; }
        public Guid? TaxCodeId { get; set; }
    }
}