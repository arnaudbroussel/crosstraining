﻿namespace crosstraining {
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
    using System.Text;
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
            //SerializeDeserialize();
            //Misc();
            //IPTools();
            //Singleton();
            //ToDate();
            GuidToBinary();
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
            ////DeserializeStringJson.DoTheThingAsync();
            DeserializeStringJson.NavigationUrl();
            ////JsonNet.Serialize();
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

        private static void ToDate() {
            var unixDate = 1613639414318;
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime date = start.AddMilliseconds(unixDate).ToLocalTime();
            Console.WriteLine(date);
        }                                              

        private static string GetFeatures(List<Guid> guids) {
            if (!guids.Any())
                return string.Empty;

            var sb = new StringBuilder();

            foreach (var guid in guids) {
                var bytes = guid.ToByteArray();

                sb.Append("0x");

                foreach (var byt in bytes)
                    sb.Append($"{byt:X2}");

                sb.Append(",");
            }

            return sb.ToString().ToLower().Remove(sb.Length - 1);
        }

        private static void GuidToBinary() {
            var guids = new List<Guid> {
                Guid.Parse("ef0a1600-97cd-1d16-dad5-1378d1457e32"),
                Guid.Parse("fb6a5401-484d-cd66-44ad-b44e242f5ee3"),
                Guid.Parse("73cadb03-ff92-2692-e21e-c66925872a71"),
                Guid.Parse("5aa14507-9c08-c86f-d09e-f8b49ad7fa87"),
                Guid.Parse("116d4807-2e24-1202-8266-17caa88182d6"),
                Guid.Parse("30df7f07-38ea-fb7c-8fdb-06d90899c00d"),
                Guid.Parse("f185c008-aa63-cd98-cc88-eb1f4bc229ed"),
                Guid.Parse("e98fab0c-0393-df08-9b9c-aed2629608bf"),
                Guid.Parse("7049310f-7ab4-aa30-6201-41e3d580daa8"),
                Guid.Parse("d312fa12-ef05-2453-dc01-927154a73279"),
                Guid.Parse("35d05814-7c82-604f-08a5-acd1be57b640"),
                Guid.Parse("d8e53015-fa45-9ce4-9d17-694ae335bc06"),
                Guid.Parse("9cd3d617-5bee-6177-9b79-37acbffe4106"),
                Guid.Parse("f9f53b18-677f-7072-09fc-b75b90ae5611"),
                Guid.Parse("5fa6f91b-106e-c3f1-c430-c20a6753ba50"),
                Guid.Parse("42bac61c-be21-ea16-3a8f-0f8fc794ec50"),
                Guid.Parse("fe27e01c-1835-64cc-ef35-70b50a9a7c10"),
                Guid.Parse("75e99a1d-079d-5cc1-2cc3-0ef0dc94276d"),
                Guid.Parse("12a9e81d-7190-718a-55d8-a363fb0f1cf9"),
                Guid.Parse("838c9c1e-ec06-2215-8447-84174b6218e3"),
                Guid.Parse("3700931f-b02e-f3bf-92f1-6929593dac9e"),
                Guid.Parse("66245221-a6e4-57d4-b14d-b39aaf7d2f77"),
                Guid.Parse("7febbc28-76af-a2cd-e266-08160f48cbb0"),
                Guid.Parse("828c0330-c027-ad9c-b651-86043ee840c0"),
                Guid.Parse("a761be30-b911-4621-f960-c758c1c87b05"),
                Guid.Parse("31ebd233-c4fd-b69a-f01b-dd66684e99d0"),
                Guid.Parse("c234f934-7ba5-ae58-d93a-d1eef7b31dea"),
                Guid.Parse("c5c87b36-508c-2f54-e73c-788ea5957d29"),
                Guid.Parse("f43def39-15b8-ba85-f069-c307f4713325"),
                Guid.Parse("1ecff139-76a6-8a9a-c006-c043cb6e3f88"),
                Guid.Parse("8ed5573c-983a-dc83-8ad7-75ed465dc0e6"),
                Guid.Parse("d1fb6942-11e0-4bd3-9f67-389acf2221b7"),
                Guid.Parse("f606f542-0357-d69a-6576-2e0e6ce6831d"),
                Guid.Parse("16cf2043-037e-e8d3-ae66-c6131594377b"),
                Guid.Parse("122e3a47-16ad-c0a8-e310-c9e5caa7673e"),
                Guid.Parse("8d8fb649-4cdc-492c-8231-72ec5ed8f406"),
                Guid.Parse("b209e64a-4457-c2a0-5918-434cb8bc8b0a"),
                Guid.Parse("f111584c-4ed4-851b-c970-e9255ad5019b"),
                Guid.Parse("76aca14c-09c8-681a-621e-cbcff41f0f85"),
                Guid.Parse("3a9b894e-814d-052c-2de7-0af0d40ba580"),
                Guid.Parse("83400c4f-190d-30ef-804d-013413703c39"),
                Guid.Parse("6a3c5b50-cca9-68f0-5669-6e27b9b762b0"),
                Guid.Parse("59b6c251-ac79-0197-06e9-a34ac9140a15"),
                Guid.Parse("80e54753-4469-0876-dca1-3310204e89d7"),
                Guid.Parse("92cc0d54-9601-4cbe-22e1-0c2f565ed55a"),
                Guid.Parse("52298558-668e-db19-097b-f77ff6dbf8d0"),
                Guid.Parse("f388bc5a-7cec-ba82-cccf-08ed9c754ff0"),
                Guid.Parse("a3b71e5b-aca2-59d7-7204-9a3dbb2029c3"),
                Guid.Parse("60e2435e-2b2f-8212-4ec5-92bcbe71d541"),
                Guid.Parse("19b6e45e-8e8b-a084-67b1-a3386a856135"),
                Guid.Parse("6bd23661-ab1f-04bb-7969-59800862f75e"),
                Guid.Parse("9d190a63-1cd9-5daa-6f33-cedc334d7c91"),
                Guid.Parse("05eaa063-a0e8-601d-8add-613113749f30"),
                Guid.Parse("4a4b4565-1267-ef60-0e56-657ca93206de"),
                Guid.Parse("ce7d4665-0c25-ceb4-f8b9-b75f7bcdc079"),
                Guid.Parse("ac29e168-9dd9-654b-789a-3c9e07fcbfc0"),
                Guid.Parse("f8b8bd6c-8827-97d2-91eb-f180291ac415"),
                Guid.Parse("5b63636d-4843-fced-0fc1-dd7dbc4282d2"),
                Guid.Parse("90e9776f-a6a7-cc8f-c955-2317aa90e61d"),
                Guid.Parse("0b6e5b72-2d29-59a0-3905-8cb22ab84eed"),
                Guid.Parse("324c677a-fa7b-f707-c120-4fb38ca6ef3b"),
                Guid.Parse("92e22d7b-c457-2861-9dff-90e284f24d63"),
                Guid.Parse("d999a97c-bfc4-a96e-a401-c5dac6ea1f3f"),
                Guid.Parse("31c6417d-c536-7d74-24d7-ce949db5265d"),
                Guid.Parse("57aa877d-dec4-fc77-4e06-0ba9d88e2288"),
                Guid.Parse("61a9d580-8602-0848-f24a-5b2b96fda538"),
                Guid.Parse("bbaf8d84-5ecd-1215-e93c-ca6d76b87e17"),
                Guid.Parse("e94e6288-e049-5aee-b42c-9939ad4d9ce1"),
                Guid.Parse("ec1f9188-9af7-2835-5c18-2af27bf5484b"),
                Guid.Parse("e099168a-2b05-87d2-1d1d-acb80df3d873"),
                Guid.Parse("ca826b8c-9f7c-e528-a8c2-0a5b4d537c53"),
                Guid.Parse("bbc2bb91-c65a-d433-b060-9b6e2ee0230a"),
                Guid.Parse("10e2e794-3454-d275-dc1e-e610e7b4af1d"),
                Guid.Parse("1857eb94-5738-3f80-0092-f4ed5629ad2b"),
                Guid.Parse("73892a95-1f17-c970-ff2c-0c827ed1fc9a"),
                Guid.Parse("0053b896-9d2d-5f8c-29e9-c4b81d6bb8de"),
                Guid.Parse("5a20d096-1000-de56-a02f-06b11533f4aa"),
                Guid.Parse("e6575998-d76d-7560-44c6-a50dd84be257"),
                Guid.Parse("27b0ba99-fdfa-0efa-e0b3-6464231361af"),
                Guid.Parse("83d26d9a-dec3-3f65-bca5-00f9721f634f"),
                Guid.Parse("a696c0a2-d479-4bf4-99a1-b02219ec97ac"),
                Guid.Parse("b681faa7-4c8a-df5c-04b5-d8e126c59c84"),
                Guid.Parse("cf06fba7-7412-b765-e9be-7822a4049445"),
                Guid.Parse("78618ba8-820c-a5c7-b7cb-d13a2fbc4b38"),
                Guid.Parse("fe9be2a9-7f07-8a9b-a791-ae125496c921"),
                Guid.Parse("d3f0abaa-5199-e6f3-c3e8-a7911df524c2"),
                Guid.Parse("7b5498ad-f0c0-68a1-9034-8394adfc6e50"),
                Guid.Parse("fcdcb2ae-1033-aaca-b820-a18a343e03ef"),
                Guid.Parse("7fce31af-1eae-c2ed-f610-5dead8413eed"),
                Guid.Parse("d95c58af-4806-eae2-c1a8-7c450a1c79c2"),
                Guid.Parse("026be9b3-8dbf-70cd-659c-641d128b275e"),
                Guid.Parse("d40e0db5-f0e0-d571-895b-2a2984f9bc07"),
                Guid.Parse("aaaf16b5-b10b-2005-71da-ab2a7aa12d20"),
                Guid.Parse("9e6819b5-3074-40b7-5ba7-a289ad60c62f"),
                Guid.Parse("b33e8db6-0c5f-c584-1030-4367614a097e"),
                Guid.Parse("35a5efb6-0f9f-4aea-0ef5-33f1fa58dd9a"),
                Guid.Parse("d527e7b8-9ac7-bc0a-9f61-e9195c2e2f68"),
                Guid.Parse("892bc7bd-2115-03d8-9bed-1cb9afec8ec1"),
                Guid.Parse("73255ebf-d4e5-6bcf-8cf5-e2c2edd7380c"),
                Guid.Parse("dfe042c0-93d1-ddb4-71a0-63731990c82f"),
                Guid.Parse("b089aac0-8ae1-1ab6-d1df-5e5acc88eae1"),
                Guid.Parse("d0a306c2-dead-7672-71ca-6736a52b8bd9"),
                Guid.Parse("1b9d0cc2-d38a-84e7-3493-f2297b02ac90"),
                Guid.Parse("630dcbc6-a112-2a88-3e82-b40b5901840d"),
                Guid.Parse("5dc67dc8-42d3-f3c5-193e-9ed232f6cb6b"),
                Guid.Parse("5fe27cce-daeb-e2d3-6610-dd844d611bbe"),
                Guid.Parse("7ecda7d2-87f6-49eb-ec83-705e402f42e7"),
                Guid.Parse("729e43d3-ad5b-22ff-d256-71aab66259ed"),
                Guid.Parse("cc4eded3-8508-fd71-48ad-8beb7af555e4"),
                Guid.Parse("e09c5dd4-dab6-ab80-a0fd-9fe96a19e242"),
                Guid.Parse("e9b780d4-8a6f-8c11-734e-e4d45ae5d136"),
                Guid.Parse("e678c6d4-1d24-53d5-c31f-4f71821368cf"),
                Guid.Parse("671f03d6-e5dc-8e3b-241d-644e35041869"),
                Guid.Parse("c4b284d7-751d-8bde-8373-76e864e17a7e"),
                Guid.Parse("c796f3da-4226-64fc-1434-936cba7f36f3"),
                Guid.Parse("ff25afdb-408f-89ef-47b5-1d3ca09b8259"),
                Guid.Parse("0c7289dc-b46e-fc12-b284-f7d50a237059"),
                Guid.Parse("6421d3dc-f3d5-cfcd-4d49-434aa92b0317"),
                Guid.Parse("dd9cfbdc-b4b3-ec2e-4115-ab8298dbc552"),
                Guid.Parse("f69871e1-9a2b-dd9d-2c09-fd7e4c9a8212"),
                Guid.Parse("6aa24ee2-700e-a64f-3945-8d7dd54a1664"),
                Guid.Parse("283853e5-0a71-7389-466c-ac1f18738fe6"),
                Guid.Parse("9b6b4ee7-5f56-54ac-00e6-8fc5a899ae20"),
                Guid.Parse("98af7fe7-bd09-7685-0ec0-15fb4ae68520"),
                Guid.Parse("cd951de8-75f1-d1de-4e88-743ba8759cf0"),
                Guid.Parse("a48113ea-9a70-c779-3f2b-de79c4538b90"),
                Guid.Parse("4640b1ed-1a59-1f7b-e7e2-91c85a9f4801"),
                Guid.Parse("d6e372ef-eb3b-d1f4-09a3-50335aa70af9"),
                Guid.Parse("47e7b1f2-e1af-3c2d-f3ca-7c4c3c7b374e"),
                Guid.Parse("ce7107f7-9409-4865-204e-4ca6a3af7d7e"),
                Guid.Parse("3e35c1f7-9b0d-2087-f1bb-e7bef5b02c54"),
                Guid.Parse("4980ddf7-d85b-1962-8bec-ee471df08259"),
                Guid.Parse("0daf42f8-dc8d-7f36-b45b-b926328441b6"),
                Guid.Parse("9c1d54f9-8be8-12ce-ad49-b00b42f09a9d"),
                Guid.Parse("b530d8f9-bed3-8ec9-e977-ef5778614920"),
                Guid.Parse("a9e47efa-7fb1-1362-831f-c4413f1ca11d"),
                Guid.Parse("e5a81bfb-6c85-05bf-f48e-53a5675b1c7d"),
                Guid.Parse("e958a1fc-4de6-51fb-3f34-03343ba2c946"),
                Guid.Parse("beab2cfd-b77a-78fe-42ac-da4e3b1a8347"),
                Guid.Parse("a32daffe-5f92-d374-d3f2-0d6e0b864312"),
                Guid.Parse("c70825ff-669c-a321-4a7f-dc99c67b10ab"),
                Guid.Parse("e67284ff-0c8e-54aa-5257-2a7a8b628a94")
            };

            Console.WriteLine(GetFeatures(guids));
        }
    }
}