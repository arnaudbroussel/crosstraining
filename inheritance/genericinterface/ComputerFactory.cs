namespace crosstraining.inheritance.genericinterface {
    public class ComputerFactory : IComputerFactory, ISpecialFactory<Computer> {
        public Computer Get() {
            return new Computer();
        }
    }
}