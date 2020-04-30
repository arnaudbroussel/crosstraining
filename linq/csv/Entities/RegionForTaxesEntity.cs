using System;

namespace crosstraining.linq.csv.Entities {
    public class RegionForTaxesEntity {
        public Guid Id { get; set; }
        public string Legislation { get; set; }
        public string Name { get; set; }
        public bool IsTheMainRegion { get; set; }
        public bool UseMainTaxes { get; set; }
        public bool UseFromTaxes { get; set; }
    }
}