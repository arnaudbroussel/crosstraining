using System;
using System.Collections.Generic;
using System.Text;

namespace crosstraining.hierarchy {
    public class Furniture : IEntity {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public override string ToString() {
            return $"{this.Name} - {this.Price} ({this.Id})";
        }
    }
}
