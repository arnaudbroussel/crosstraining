using System;
using System.Collections.Generic;
using System.Text;

namespace crosstraining.hierarchy {
    public class People : IEntity {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString() {
            return $"{this.Name} ({this.Id})";
        }
    }
}