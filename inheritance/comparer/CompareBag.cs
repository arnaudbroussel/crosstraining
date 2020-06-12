using System;
using System.Collections.Generic;
using System.Text;

namespace crosstraining.inheritance.comparer {
    public class CompareBag {

        public void OrderStringList() {
            List<string> strings = new List<string>() { "SEBADOH", "Shellac", "Galaxie 500", "ARAB STRAP" };
            strings.Sort();
            foreach (var s in strings) {
                Console.WriteLine($"{s}");
            }
        }

        public void OrderObjectList() {
            List<Employee> employees = new List<Employee>() { 
                new Employee() { Name = "Lou", Category = 12},
                new Employee() { Name = "Wayne", Category = 8},
                new Employee() { Name = "Ada", Category = 8},
                new Employee() { Name = "Pat", Category = 2},
                new Employee() { Name = "Manu", Category = 20},
            };
            employees.Sort();
            foreach (var s in employees) {
                Console.WriteLine($"{s}");
            }
        }
    }
}