namespace crosstraining.events {
    using System;

    public class ThingsToDo {
        private static void Show(MultipleOfEventArgs e) {
            Console.WriteLine($"{e.Value} is a multiple of {e.Text} reached! ");
        }

        public static void ProcessMultipleOfFiveReached(object sender, MultipleOfEventArgs e) { Show(e); }

        public static void ProcessMultipleOfTenReached(object sender, MultipleOfEventArgs e) { Show(e); }

        public static void ProcessMultipleOfOtherReached(object sender, MultipleOfEventArgs e) { Show(e); }

        public static void ProcessSourceLambda(string text) { Console.WriteLine($"LAMBDA SOURCE CALLEDS {text}"); }

        public static void ProcessSourceLambda(MultipleOfEventArgs e, string text) { Console.WriteLine($"LAMBDA SOURCE CALLEDS {text} for {e.Value} / {e.Text}"); }
    }
}