namespace crosstraining.inheritance.genericinterface {
    using System;

    public class Computer : ICloneable {
        public object Clone() {
            return new Computer();
        }
    }
}