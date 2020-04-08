using crosstraining.linq.csv.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace crosstraining.linq.csv {
    public class MockData {

        private List<RegionForTaxesEntity> regionForTaxesEntityList;
        private List<TaxRateEntity> taxRateEntityList;
        private List<TaxTreatmentEntity> taxTreatmentEntityList;

        public List<RegionForTaxesEntity> RegionForTaxesEntityList {
            get {
                if (this.regionForTaxesEntityList == null) {
                    this.regionForTaxesEntityList = LoadData.ExtractRegionForTaxesFromCSV("./linq/csv/data/RegionForTaxes.csv");
                }
                return this.regionForTaxesEntityList;
            }
        }

        public List<TaxRateEntity> TaxRateEntityList {
            get {
                if (this.taxRateEntityList == null) {
                    this.taxRateEntityList = LoadData.ExtractTaxRatesFromCSV("./linq/csv/data/TaxRates.csv");
                }
                return this.taxRateEntityList;
            }
        }

        public List<TaxTreatmentEntity> TaxTreatmentEntityList {
            get {
                if (this.taxTreatmentEntityList == null) {
                    this.taxTreatmentEntityList = LoadData.ExtractTaxTreatmentsFromCSV("./linq/csv/data/TaxTreatments.csv");
                }
                return this.taxTreatmentEntityList;
            }
        }
    }
}