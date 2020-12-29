namespace crosstraining {
    using crosstraining.events;
    using crosstraining.hierarchy;
    using crosstraining.inheritance.comparer;
    using crosstraining.inheritance.genericinterface;
    using crosstraining.inheritance.useinterface;
    using crosstraining.linq.csv;
    using crosstraining.linq.csv.Entities;
    using crosstraining.Network;
    using crosstraining.reflection;
    using crosstraining.SerializeDeserialize;
    using crosstraining.singleton;
    ////using SBC.Accounting.Tax.Service.PerformanceTesting.Setup;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    class Program {
        private static Dictionary<string, object> commandArgs = new Dictionary<string, object> {
                { "-threads", 1},
                { "-runtime", 1},
                { "-size", null},
                { "-operations", new List<string>()}
            };

        private static string[] availableOperations = new string[] {
            "tax-code-create", "tax-code-getbyid", "tax-code-getall", "tax-code-update", "tax-code-delete",
            "tax-group-create", "tax-group-getbyid", "tax-group-getall", "tax-group-update", "tax-group-delete",
        };

        private static bool showDefault;

        static void Main(string[] args) {
            //GeneratedGuids();
            //PrintListToString();
            //GetArguments(args);
            //UseGenericInterfaces();
            //UseInterfaces();
            //EventsAndHandlers();
            //ActionsOfSomething();
            //LinqCSV();
            //Repository();
            //Comparer();
            //Pagination();
            //UsingReflection();
            //SetupDatabase();
            SerializeDeserialize();
            //Misc();
            //IPTools();
            //Singleton();
        }

        private static bool GetArguments(string[] args) {
            if (args.Contains("-h") || args.Contains("-help"))
                return PrintUsage(availableOperations);

            List<string> operations = (List<string>) commandArgs["-operations"];
            string currentArg = string.Empty;
            foreach (string arg in args) {
                if (arg.StartsWith('-')) {
                    if (!commandArgs.ContainsKey(arg)) {
                        return PrintArgumentError($"The argument {arg} is not valid.");
                    }
                    currentArg = arg;
                }
                else {
                    if (currentArg != "-operations") {
                        if (Int32.TryParse(arg, out int toIntValue)) {
                            commandArgs[currentArg] = toIntValue;
                        }
                        else {
                            return PrintArgumentError($"The value {arg} is not valid for the parameter {currentArg}.");
                        }
                    }
                    else {
                        if (availableOperations.Contains(arg))
                            operations.Add(arg);
                        else
                            return PrintArgumentError($"The operation {arg} is not available. See documentation with -help.");
                    }
                }
            }
            if (commandArgs["-size"] == null)
                return PrintArgumentError($"The -size paramater is required.");
            if (operations.Count > 0)
                return true;
            else
                return PrintArgumentError($"No operation requested.");
        }

        private static bool PrintArgumentError(string message) {
            Console.WriteLine(message);
            return false;
        }

        private static bool PrintUsage(string[] availableOperations) {
            Console.WriteLine("Valid arguments:");
            foreach (string key in commandArgs.Keys)
                Console.WriteLine($"\t{key}");
            Console.WriteLine("\r\nAvailable operations:");
            foreach (string availableOperation in availableOperations)
                Console.WriteLine($"\t{availableOperation}");

            return false;
        }

        private static void UseGenericInterfaces() {
            IComputerFactory factory = new ComputerFactory();
            var computer = factory.Get();
            Console.WriteLine(computer.GetType());
            Console.WriteLine(computer.Clone().GetType());
        }

        private static void PrintListToString() {
            var names = new List<string>() { "John", "Anna", "Monica" };
            var joinedNames = names.Aggregate((a, b) => a + ", " + b);
            Console.WriteLine($"joinedNames = {joinedNames}");
            Console.WriteLine($"availableOperations = {availableOperations}");

            string strRegex = @"[ ](?=(?:[^""]*""[^""]*"")*[^""]*$)";
            Regex myRegex = new Regex(strRegex, RegexOptions.Multiline);
            string strTargetString = @"-size 100 -operations tax-treatment-create tax-treatment-update tax-treatment-get-all tax-treatment-get-by-id -runtime 1 -name ""Tax Treatment -Thread number = 1"" -threads 1";

            var s = myRegex.Split(strTargetString);
            foreach (var item in s) {
                Console.WriteLine(item.ToString().Replace("\"", ""));
            }
        }

        private static void UseInterfaces() {
            IAnimal animal2 = new Dog();
            IAnimal animal = new Dog();
            animal.Move();
            Dog dog = (Dog) animal;
            dog.Bark();
            MoveAnimal(animal);
            MoveAnimal(dog);

            Console.WriteLine(animal2.GetHashCode());
            Console.WriteLine(animal.GetHashCode());
            Console.WriteLine(dog.GetHashCode());
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
            a.OnMultipleOfTenReached += (s, e) => ThingsToDo.ProcessSourceLambda("MARK LANEGAN");

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
                Console.WriteLine($"{e.Id} {e.Name} {e.Legislation} {e.IsTheMainRegion} {e.UseFromTaxes} {e.UseMainTaxes}");
            }
            Console.WriteLine("------------\n");
            foreach (var e in TaxRateEntityList) {
                Console.WriteLine($"{e.Id} {e.Legislation} {e.Name} {e.RegionForTaxes} {e.TaxTreatment} {e.TaxItemType} {e.TaxCode} {e.Rate} {e.RegionForTaxesId} {e.TaxTreatmentId} {e.TaxItemTypeId} {e.TaxCodeId}");
            }
            Console.WriteLine("------------\n");
            foreach (var e in TaxTreatmentEntityList) {
                Console.WriteLine($"{e.Id} {e.Legislation} {e.TaxTreatment} {e.RegionForTaxes} {e.UseFromTaxes} {e.RegionForTaxesId}");
            }
            Console.WriteLine("------------\n");
        }

        private static void Repository() {
            List<People> people = new List<People>() {
                new People{ Id = 1, Name =  "IRIA" },
                new People{ Id = 2, Name =  "NIL" },
                new People{ Id = 3, Name =  "LUCA" },
            };

            List<Furniture> furnitures = new List<Furniture>() {
                new Furniture{ Id = 1, Name =  "CHAIR", Price = 1.23 },
                new Furniture{ Id = 2, Name =  "DESKTOP", Price= 4.5 },
                new Furniture{ Id = 3, Name =  "TABLE", Price = 15.15 },
                new Furniture{ Id = 4, Name =  "CUPBOARD", Price = 0.45 },
            };

            IRepository<People> repoPeople = new Repository<People>(people);
            IRepository<Furniture> repoFurniture = new Repository<Furniture>(furnitures);

            people.Add(new People { Id = 4, Name = "MURIEL" });
            people.Add(new People { Id = 5, Name = "ARNAUD" });

            Console.WriteLine($"{repoPeople.FindById(1)}");
            Console.WriteLine($"{repoPeople.FindById(2)}");
            Console.WriteLine($"{repoPeople.FindById(3)}");
            Console.WriteLine("------------\n");
            foreach (var p in repoPeople.All())
                Console.WriteLine($"{p}");
            Console.WriteLine("------------\n");
            Console.WriteLine("------------\n");
            foreach (var f in repoFurniture.All())
                Console.WriteLine($"{f}");
        }

        private static void Comparer() {
            CompareBag compareBag = new CompareBag();
            compareBag.OrderStringList();
            Console.WriteLine("------------\n");
            compareBag.OrderObjectList();
        }

        public struct Pagined {
            public int pages;
            public int last;
            public int max;

            public int steps(int i) {
                if (i < (pages - 1) && pages != 1)
                    return max;
                else if (i > pages)
                    return 0;
                else {
                    return last;
                }
            }
        }

        private static Pagined Pages(int max, int size) {
            Pagined s = new Pagined();

            s.pages = 1;
            s.last = size;
            s.max = max;

            if (size > max)
                s.pages = (size / max) + ((size % max) > 0 ? 1 : 0);

            if (s.pages != 1) {
                s.last = (size % max);
            }

            return s;
        }

        private static void Pagination() {
            Pagined s = Pages(1000, 3125);
            for (int i = 0; i < s.pages; i++)
                Console.WriteLine(i + " > " + s.steps(i));
            Console.WriteLine("-----");

            s = Pages(1000, 999);
            for (int i = 0; i < s.pages; i++)
                Console.WriteLine(i + " > " + s.steps(i));
            Console.WriteLine("-----");

            s = Pages(1000, 3001);
            for (int i = 0; i < s.pages; i++)
                Console.WriteLine(i + " > " + s.steps(i));
            Console.WriteLine("-----");
        }

        private static void UsingReflection() {
            Console.WriteLine(WorkWithReflection.GetCurrentMethod());

            Furniture furniture = new Furniture { Id = 1, Name = "CHAIR", Price = 1.23 };
            Console.WriteLine("Ex 1 Dump an object:");
            WorkWithReflection.DumpObject(furniture);
            Console.WriteLine();

            Console.WriteLine("Ex 2 Invoke methods:");
            WorkWithReflection.InvokeMethods(furniture);
            Console.WriteLine();

            List<Employee> employees = new List<Employee>() {
                new Employee() { Name = "Lou", Category = 12},
                new Employee() { Name = "Wayne", Category = 8},
            };
            Console.WriteLine("Ex 3 Invoke other methods:");
            WorkWithReflection.InvokeMethodNoArgument(employees[0], employees[1], "CompareTo", typeof(Employee));
            Console.WriteLine();

            WorkWithReflection.CompanyTestAttribute();

            WorkWithReflection.GetFolderClasses();
        }

        private static void GeneratedGuids() {
            for (int i = 0; i < 8000; i++) {
                Console.WriteLine($"\"{Guid.NewGuid()}\",");
            }
        }

        private static void SetupDatabase() {
            for (int i = 0; i < 500; i++) {
                Console.WriteLine($"\"{Guid.NewGuid()}\",");
            }

            ////Setup.SetupDatabase().Wait();
        }

        private static readonly Func<int, int, int> InsertTemplatePages = (numberOfTemplates, numberOfQueriesAtOnce) => (int) Math.Ceiling(((double) numberOfTemplates / numberOfQueriesAtOnce));

        private static void SerializeDeserialize() {
            ////WorkWithSerialize.SerializeObjectAndRestoreFromXML();
            ////JsonNet.Serialize();
            ////JsonNet.SerializePerformanceTesting();
            DeserializeStringJson.DoTheThing();
        }

        private static void Misc() {
            Stack<char> pile = new Stack<char>();
            pile.Push('A');
            pile.Push('B');
            pile.Push('C');
            Console.WriteLine(pile.ToArray()[0]);
            Console.WriteLine(pile.ToArray()[1]);
            Console.WriteLine(pile.ToArray()[2]);
            
            Console.WriteLine(showDefault);
        }

        private static void IPTools() {
            var iptools = new IPTools();
            iptools.GetIP();
        }

        private static void Singleton() {
            RegulatoryReportingClient.SendAsync();
            //for (int i = 0; i < 100000; i++) {
            //    RegulatoryReportingClient.SendAsync();
            //}
        }
    }
}