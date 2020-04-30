using System;
using System.Collections.Generic;

namespace crosstraining.linq.csv.Entities.Extensions {
    public static class TaxRateEntityExtension {
        public static IEnumerable<TaxRateEntity> ToTaxRateEntity(this IEnumerable<string> source, Dictionary<LoadData.RelationType, Dictionary<string, Guid>> primayKeys) {
            foreach(var item in source) {
                var columns = item.Split(',');

                yield return new TaxRateEntity {
                    Id = Guid.NewGuid(),
                    Legislation = columns[1],
                    Name = columns[2],
                    RegionForTaxes = columns[3],
                    TaxTreatment = columns[4],
                    TaxItemType = columns[5],
                    TaxCode = columns[6],
                    Rate = double.Parse(columns[7]),
                    RegionForTaxesId = primayKeys[LoadData.RelationType.RegionForTaxes].ContainsKey(columns[3]) ? primayKeys[LoadData.RelationType.RegionForTaxes][columns[3]] : (Guid?) null,
                    TaxTreatmentId = primayKeys[LoadData.RelationType.TaxTreatments].ContainsKey(columns[4]) ? primayKeys[LoadData.RelationType.TaxTreatments][columns[4]] : (Guid?) null,
                    TaxItemTypeId = primayKeys[LoadData.RelationType.TaxItemTypes].ContainsKey(columns[5]) ? primayKeys[LoadData.RelationType.TaxItemTypes][columns[5]] : (Guid?) null,
                    TaxCodeId = primayKeys[LoadData.RelationType.TaxCodes].ContainsKey(columns[6]) ? primayKeys[LoadData.RelationType.TaxCodes][columns[6]] : (Guid?) null,
                };
            }
        }
    }
}