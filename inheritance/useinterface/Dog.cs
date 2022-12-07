namespace crosstraining.inheritance.useinterface {
    using System;

    public class Dog : IAnimal {
        public void Move() { Console.WriteLine(this.GetType() + " Move()"); }
        public void Bark() { Console.WriteLine(this.GetType() + " Bark()"); }
    }
}