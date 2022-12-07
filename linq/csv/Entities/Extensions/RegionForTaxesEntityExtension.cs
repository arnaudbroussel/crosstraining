using System;
using System.Collections.Generic;

namespace crosstraining.linq.csv.Entities.Extensions {
    public static class RegionForTaxesEntityExtension {
        public static IEnumerable<RegionForTaxesEntity> ToRegionForTaxesEntity(this IEnumerable<string> source) {
            foreach(var item in source) {
                var columns = item.Split(',');

                yield return new RegionForTaxesEntity {
                    Id = Guid.NewGuid(),
                    Legislation = columns[0],
                    Name = columns[1],
                    IsTheMainRegion = (columns[2] == "No"),
                    UseMainTaxes = (columns[3] == "No"),
                    UseFromTaxes = (columns[4] == "No")
                };
            }
        }
    }
}