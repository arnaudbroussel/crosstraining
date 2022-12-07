namespace crosstraining.inheritance.genericinterface {
    public interface IComputerFactory : IFactory<Computer> {
        Computer Get();
    }
}