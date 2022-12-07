using crosstraining.linq.csv.Entities;
using crosstraining.linq.csv.Entities.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace crosstraining.linq.csv {
    public class LoadData {

        public enum RelationType { RegionForTaxes, TaxTreatments, TaxItemTypes, TaxCodes, TaxRates };

        private Dictionary<RelationType, Dictionary<string, Guid>> PrimaryKeys = new Dictionary<RelationType, Dictionary<string, Guid>>();

        private List<RegionForTaxesEntity> regionForTaxesEntityList;
        private List<TaxTreatmentEntity> taxTreatmentEntityList;
        private List<TaxRateEntity> taxRateEntityList;

        public List<RegionForTaxesEntity> RegionForTaxesEntityList { get { return this.regionForTaxesEntityList; } }
        public List<TaxTreatmentEntity> TaxTreatmentEntityList { get { return this.taxTreatmentEntityList; } }
        public List<TaxRateEntity> TaxRateEntityList { get { return this.taxRateEntityList; } }

        public void AddPrimaryKey(RelationType relationType, string key, Guid id) {
            if (this.PrimaryKeys.ContainsKey(relationType))
                this.PrimaryKeys[relationType].Add(key, id);
            else
                this.PrimaryKeys.Add(relationType, new Dictionary<string, Guid>() { { key, id } });
        }

        public void ExtractRegionForTaxesFromCSV(string path) {
            var query = File.ReadAllLines(path)
                       .Skip(1)
                       .Where(l => l.Length > 1)
                       .ToRegionForTaxesEntity();
            this.regionForTaxesEntityList = query.ToList();

            this.PrimaryKeys.Add(RelationType.RegionForTaxes, this.regionForTaxesEntityList.ToDictionary(x => x.Name, x => x.Id));
        }

        public void ExtractTaxRatesFromCSV(string path) {
            var query = File.ReadAllLines(path)
                       .Skip(1)
                       .Where(l => l.Length > 1)
                       .ToTaxRateEntity(this.PrimaryKeys);
            this.taxRateEntityList = query.ToList();

            this.PrimaryKeys.Add(RelationType.TaxRates, this.taxRateEntityList.ToDictionary(x => x.Name, x => x.Id));
        }

        public void ExtractTaxTreatmentsFromCSV(string path) {
            var query = File.ReadAllLines(path)
                       .Skip(1)
                       .Where(l => l.Length > 1)
                       .ToTaxTreatmentEntity(this.PrimaryKeys);

            this.taxTreatmentEntityList = query.ToList();

            this.PrimaryKeys.Add(RelationType.TaxTreatments, this.taxTreatmentEntityList.ToDictionary(x => x.TaxTreatment, x => x.Id));
        }
    }
}