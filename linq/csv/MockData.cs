using crosstraining.linq.csv.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crosstraining.linq.csv {
    public class MockData {
        private LoadData dataLoader;

        public MockData() {
            this.dataLoader = new LoadData();
            this.LoadScenarioData();
        }

        private void LoadScenarioData() {
            this.dataLoader.AddPrimaryKey(LoadData.RelationType.TaxCodes, "Taxable", Guid.NewGuid());
            this.dataLoader.AddPrimaryKey(LoadData.RelationType.TaxCodes, "IVA Normal", Guid.NewGuid());
            this.dataLoader.AddPrimaryKey(LoadData.RelationType.TaxCodes, "IGIC Normal", Guid.NewGuid());
            this.dataLoader.AddPrimaryKey(LoadData.RelationType.TaxCodes, "Standard Rate", Guid.NewGuid());

            this.dataLoader.AddPrimaryKey(LoadData.RelationType.TaxItemTypes, "Goods", Guid.NewGuid());

            this.dataLoader.ExtractRegionForTaxesFromCSV("./linq/csv/data/RegionForTaxes.csv");
            this.dataLoader.ExtractTaxTreatmentsFromCSV("./linq/csv/data/TaxTreatments.csv");
            this.dataLoader.ExtractTaxRatesFromCSV("./linq/csv/data/TaxRates.csv");
        }

        public List<RegionForTaxesEntity> RegionForTaxesEntityList {
            get {
                return this.dataLoader.RegionForTaxesEntityList;
            }
        }

        public List<TaxTreatmentEntity> TaxTreatmentEntityList {
            get {
                return this.dataLoader.TaxTreatmentEntityList;
            }
        }

        public List<TaxRateEntity> TaxRateEntityList {
            get {
                return this.dataLoader.TaxRateEntityList;
            }
        }
    }
}