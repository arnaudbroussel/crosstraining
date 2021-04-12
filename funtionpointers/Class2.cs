using System;
using System.Collections.Generic;
using System.Text;

namespace crosstraining.funtionpointers {
    public class Class2: BaseEntity {

        string NickName { get; set; }

        public Class2 GetBody() {
            return new Class2 { NickName = "Vega" };
        }
    }
}
