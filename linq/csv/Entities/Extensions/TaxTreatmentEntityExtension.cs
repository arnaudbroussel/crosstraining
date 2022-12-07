using System;
using System.Collections.Generic;

namespace crosstraining.linq.csv.Entities.Extensions {
    public static class TaxTreatmentEntityExtension {
        public static IEnumerable<TaxTreatmentEntity> ToTaxTreatmentEntity(this IEnumerable<string> source, Dictionary<LoadData.RelationType, Dictionary<string, Guid>> primayKeys) {
            foreach(var item in source) {
                var columns = item.Split(',');

                yield return new TaxTreatmentEntity {
                    Id = Guid.NewGuid(),
                    Legislation = columns[1],
                    TaxTreatment = columns[2],
                    RegionForTaxes = columns[3],
                    UseFromTaxes = (columns[4] == "No"),
                    RegionForTaxesId = primayKeys[LoadData.RelationType.RegionForTaxes].ContainsKey(columns[3]) ? primayKeys[LoadData.RelationType.RegionForTaxes][columns[3]] : (Guid?) null
                };
            }
        }
    }
}