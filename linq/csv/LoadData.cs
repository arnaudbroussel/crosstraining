using crosstraining.linq.csv.Entities;
using crosstraining.linq.csv.Entities.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace crosstraining.linq.csv {
    public class LoadData {

        public static List<RegionForTaxesEntity> ExtractRegionForTaxesFromCSV(string path) {
            var query = File.ReadAllLines(path)
                       .Skip(1)
                       .Where(l => l.Length > 1)
                       .ToRegionForTaxesEntity();
            return query.ToList();
        }

        public static List<TaxRateEntity> ExtractTaxRatesFromCSV(string path) {
            var query = File.ReadAllLines(path)
                       .Skip(1)
                       .Where(l => l.Length > 1)
                       .ToTaxRateEntity();
            return query.ToList();
        }

        public static List<TaxTreatmentEntity> ExtractTaxTreatmentsFromCSV(string path) {
            var query = File.ReadAllLines(path)
                       .Skip(1)
                       .Where(l => l.Length > 1)
                       .ToTaxTreatmentEntity();
            return query.ToList();
        }
    }
}