using System;
using System.Collections.Generic;

namespace crosstraining.linq.csv.Entities.Extensions {
    public static class TaxTreatmentEntityExtension {
        public static IEnumerable<TaxTreatmentEntity> ToTaxTreatmentEntity(this IEnumerable<string> source) {
            foreach(var item in source) {
                var columns = item.Split(',');

                yield return new TaxTreatmentEntity {
                    Id = Guid.Parse(columns[0]),
                    Legislation = columns[1],
                    TaxTreatment = columns[2],
                    RegionForTaxes = columns[3],
                    UseFromTaxes = (columns[4] == "No")
                };
            }
        }
    }
}