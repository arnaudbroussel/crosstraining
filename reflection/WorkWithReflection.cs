using crosstraining.inheritance.comparer;
using System;
using System.Reflection;
using System.Threading;

namespace crosstraining.reflection {
    class WorkWithReflection {

        public static void DumpObject(object obj) {
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
            MethodInfo compareToMethod = obj1.GetType().GetMethod(methodName, new Type[] { t });
            var method = compareToMethod.Invoke(obj1, new object[] { obj2 });
            Console.WriteLine($"result = {method}");
        }
    }
}
