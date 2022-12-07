using System;
using System.Collections.Generic;
using System.Text;

namespace crosstraining.funtionpointers {
    public class Class1 : BaseEntity{

        string Name { get; set;  } 

        public List<Class1> GetBody() {
            return new List<Class1> { new Class1 { Name = "Arnaud" }, new Class1 { Name = "Iria" } };
        }

        public void Print() {
            ////base.Print();
        }
    }
}
