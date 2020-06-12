using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Text;

namespace crosstraining.inheritance.comparer {
    public class Employee : IComparable<Employee> {

        public string Name { get; set; }
        public int Category { get; set; }

        public int CompareTo(Employee other) {
            return this.Category.CompareTo(other.Category);
        }

        public override string ToString() {
            return $"{this.Name} - Category: {this.Category}";
        }
    }
}