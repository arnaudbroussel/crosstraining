namespace crosstraining.inheritance.genericinterface {
    using System;

    public interface ISpecialFactory<T> : IFactory<T> where T : ICloneable {
        T Get();
    }
}