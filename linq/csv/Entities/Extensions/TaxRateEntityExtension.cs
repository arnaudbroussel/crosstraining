using System;
using System.Collections.Generic;

namespace crosstraining.linq.csv.Entities.Extensions {
    public static class TaxRateEntityExtension {
        public static IEnumerable<TaxRateEntity> ToTaxRateEntity(this IEnumerable<string> source) {
            foreach(var item in source) {
                var columns = item.Split(',');

                yield return new TaxRateEntity {
                    Id = Guid.Parse(columns[0]),
                    Legislation = columns[1],
                    Name = columns[2],
                    RegionForTaxes = columns[3],
                    TaxTreatment = columns[4],
                    TaxItemType = columns[5],
                    TaxCode = columns[6],
                    Rate = double.Parse(columns[7])
                };
            }
        }
    }
}