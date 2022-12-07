using crosstraining.hierarchy;
using crosstraining.inheritance.comparer;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace crosstraining.reflection {
    public static class WorkWithReflection {

        public static void DumpObject(object obj) {
            Console.WriteLine(WorkWithReflection.GetCurrentMethod());

            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (FieldInfo field in fields) {
                if (field.FieldType == typeof(int)) {
                    Console.WriteLine($"The field {field.Name} is an int. Value = {field.GetValue(obj)}");
                }
                else {
                    Console.WriteLine($"The field {field.Name} is NOT an int but {field.FieldType}. Value = {field.GetValue(obj)}");
                }
            }
        }

        public static void InvokeMethods(object obj) {
            Console.WriteLine(WorkWithReflection.GetCurrentMethod());

            MethodInfo toStringMethod = obj.GetType().GetMethod("ToString");
            string toString = (string) toStringMethod.Invoke(obj, null);
            Console.WriteLine($"result = {toString}");

            MethodInfo sumIntegersMethod = obj.GetType().GetMethod("SumIntegers");
            int sumIntegers = (int) sumIntegersMethod.Invoke(obj, new object[] { true });
            Console.WriteLine($"result = {sumIntegers}");

            sumIntegers = (int) sumIntegersMethod.Invoke(obj, new object[] { false });
            Console.WriteLine($"result = {sumIntegers}");
        }

        public static void InvokeMethodNoArgument(object obj1, object obj2, string methodName, Type t) {
            Console.WriteLine(WorkWithReflection.GetCurrentMethod());

            MethodInfo compareToMethod = obj1.GetType().GetMethod(methodName, new Type[] { t });
            var method = compareToMethod.Invoke(obj1, new object[] { obj2 });
            Console.WriteLine($"result = {method}");
        }

        [AttributeUsage(AttributeTargets.Class)]
        public class CompanyAttribute : Attribute {
            private string _name;
            private string _location;
            public string Name { get => _name; set => _name = value; }
            public string Location { get => _location; set => _location = value; }
            public CompanyAttribute(string company, string location) { Name = company; Location = location; }
        }

        [AttributeUsage(AttributeTargets.Class)]
        public class BiduleAttribute : Attribute {
            private string _name;
            private string _location;
            public string Name { get => _name; set => _name = value; }
            public string Location { get => _location; set => _location = value; }
            public BiduleAttribute(string company, string location) { Name = company; Location = location; }
        }

        [Company("Microsoft", "USA")]
        [Bidule("XYZ", "123")]
        public class Server {
            private string _name;                                                                                    
            public string Name { get => _name; set => _name = value; }
            public Server(string name) { Name = name; }
            [Obsolete("Do not use the UpdateServer() method, use the method UpdateAndRestart()", false)]
            public void UpdateServer() { Console.WriteLine($"Downloading and installing updates on server: {Name}!"); }
            public void RestatServer() { Console.WriteLine($"Gracefully restarting the server: {Name}"); }
            public void UpdateAndRestart() { Console.WriteLine($"Installing latest updates on machine: {Name} and restarting"); }
        }

        public static void CompanyTestAttribute() {
            Console.WriteLine(WorkWithReflection.GetCurrentMethod());

            MemberInfo info = typeof(Server);

            Console.WriteLine(((CompanyAttribute) info.GetCustomAttributes(typeof(CompanyAttribute), false)[0]).Name);

            object[] attrib = info.GetCustomAttributes(typeof(CompanyAttribute), false);
            foreach (Object attribute in attrib) {
                CompanyAttribute b = (CompanyAttribute) attribute;
                Console.WriteLine($"{b.Name}, {b.Location}");
            }

            Server a = new Server("Domain Controller");
            a.UpdateServer();
        }

        public static void GetFolderClasses() {
            Console.WriteLine(WorkWithReflection.GetCurrentMethod());

            Console.WriteLine();
            var myType = typeof(Furniture);
            Console.WriteLine($"Furniture namespace = {myType.Namespace}");
            var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == myType.Namespace && !t.IsAbstract && !t.IsInterface
                    select t;
            q.ToList().ForEach(t => Console.WriteLine(t.Name));
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod() {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }
    }
}
