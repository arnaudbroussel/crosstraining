namespace crosstraining {
    using crosstraining.events;
    using crosstraining.inheritance.genericinterface;
    using crosstraining.inheritance.useinterface;
    using System;

    class Program {
        static void Main(string[] args) {
            //UseGenericInterfaces();
            //UseInterfaces();
            EventsAndHandlers();
        }

        private static void UseGenericInterfaces() {
            IComputerFactory factory = new ComputerFactory();
            var computer = factory.Get();
            Console.WriteLine(computer.GetType());
            Console.WriteLine(computer.Clone().GetType());
        }

        private static void UseInterfaces() {
            IAnimal animal = new Dog();
            animal.Move();
            Dog dog = (Dog) animal;
            dog.Bark();
            MoveAnimal(animal);
            MoveAnimal(dog);
        }

        private static void MoveAnimal(IAnimal animal) {
            animal.Move();
        }

        private static void EventsAndHandlers() {
            Adder a = new Adder();

            a.OnMultipleOfFiveReached += Adder.a_MultipleOfFiveReached;
            a.OnMultipleOfTenReached += Adder.a_MultipleOfTenReached;
            a.OnMultipleOfOtherReached += Adder.a_MultipleOfOtherReached;

            int iAnswer = a.Add(4, 3);
            Console.WriteLine("------------------------");

            iAnswer = a.Add(4, 6);
            Console.WriteLine("------------------------");

            iAnswer = a.Add(5, 20);
            Console.WriteLine("------------------------");

            iAnswer = a.Add(5, 25);
            Console.WriteLine("------------------------");

            iAnswer = a.Add(1, 1);
            Console.WriteLine("------------------------");
        }
    } 
}