using System;
using System.Collections.Generic;
using System.Text;

namespace crosstraining.inheritance.genericinterface {
    public interface IFactory<T> {
        T Get();
    }
}