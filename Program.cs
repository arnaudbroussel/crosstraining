namespace crosstraining {
    using crosstraining.events;
    using crosstraining.inheritance.genericinterface;
    using crosstraining.inheritance.useinterface;
    using crosstraining.linq.csv;
    using crosstraining.linq.csv.Entities;
    using System;
    using System.Collections.Generic;

    class Program {
        static void Main(string[] args) {
            //UseGenericInterfaces();
            //UseInterfaces();
            //EventsAndHandlers();
            //ActionsOfSomething();
            LinqCSV();
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

            a.OnMultipleOfFiveReached += ThingsToDo.ProcessMultipleOfFiveReached;

            // same event to process several actions
            a.OnMultipleOfTenReached += ThingsToDo.ProcessMultipleOfTenReached;
            // anonymous (no need to refer to a method to be executed with parameters)
            a.OnMultipleOfTenReached += (s, e) => ThingsToDo.ProcessSourceLambda(e, "TEN");

            // below several examples to attach an action to be performed on an event:
            // (actually here, 1 event raises several actions)
            a.OnMultipleOfOtherReached += ThingsToDo.ProcessMultipleOfOtherReached;
            a.OnMultipleOfOtherReached += new EventHandler<MultipleOfOtherEventArgs>(ThingsToDo.ProcessMultipleOfOtherReached);
            // anonymous --> here is "live attachment"...
            a.OnMultipleOfOtherReached += delegate (object sender, MultipleOfOtherEventArgs e) {
                Console.WriteLine($"ANONYMOUS METHOD --> {e.Value} is a multiple of {e.Text} reached! ");
            };
            // lambda --> here is "live attachment"...
            a.OnMultipleOfOtherReached += (s, e) => {
                Console.WriteLine($"LAMBDA METHOD --> {e.Value} is a multiple of {e.Text} reached! ");
            };

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

        private static void ActionsOfSomething() {
            Adder a = new Adder();

            Action<int, int> SumActionFor1000 = (x, y) => {
                x = x * 1000;
                y++;
                Console.WriteLine($"Local action {x + y}");
            };

            // use in other class actions
            a.ProcessAction(10, 1, a.SumActionFor10);
            a.ProcessAction(10, 1, a.SumActionFor20);
            a.ProcessAction(1, 1, a.SumActionForOther);
            a.ProcessAction(11, 1, a.SumActionForOther);

            // user local action
            a.ProcessAction(10, 1, SumActionFor1000);
        }

        private static void LinqCSV() {
            MockData MockData = new MockData();
            List<RegionForTaxesEntity> RegionForTaxesEntityList = MockData.RegionForTaxesEntityList;
            List<TaxRateEntity> TaxRateEntityList = MockData.TaxRateEntityList;
            List<TaxTreatmentEntity> TaxTreatmentEntityList = MockData.TaxTreatmentEntityList;

            foreach (var e in RegionForTaxesEntityList) {
                Console.WriteLine($"{e.Name} {e.Legislation} {e.IsTheMainRegion} {e.UseFromTaxes} {e.UseMainTaxes}");
            }
            foreach (var e in TaxRateEntityList) {
                Console.WriteLine($"{e.Id} {e.Legislation} {e.Name} {e.RegionForTaxes} {e.TaxTreatment} {e.TaxItemType} {e.TaxCode} {e.Rate}");
            }
            foreach (var e in TaxTreatmentEntityList) {
                Console.WriteLine($"{e.Id} {e.Legislation} {e.TaxTreatment} {e.RegionForTaxes} {e.UseFromTaxes}");
            }
        }
    }
}