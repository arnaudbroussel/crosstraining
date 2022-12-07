using crosstraining.reflection;
using System;
using System.Collections.Generic;
using System.Text;

namespace crosstraining.hierarchy {
    public class Furniture : IEntity {

        public Furniture() {
            this.HiddenInt = 500;
            this.OtherHiddenInt = 951;
            this.VisibleInt = 499;

            Console.WriteLine(WorkWithReflection.GetCurrentMethod());
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        private int HiddenInt { get; set; }
        private int OtherHiddenInt { get; set; }
        public int VisibleInt { get; set; }

        public override string ToString() {
            return $"{this.Name} - {this.Price} ({this.Id})";
        }

        public int SumIntegers(bool onlyPublic = false) {
            return this.VisibleInt + ((onlyPublic) ? 1 : 0) * (this.HiddenInt + this.OtherHiddenInt);
        }
    }
}
