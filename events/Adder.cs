namespace crosstraining.events {
    using System;

    public class Adder {
        public event EventHandler<MultipleOfFiveEventArgs> OnMultipleOfFiveReached;
        public event EventHandler<MultipleOfTenEventArgs> OnMultipleOfTenReached;
        public event EventHandler<MultipleOfOtherEventArgs> OnMultipleOfOtherReached;

        public int Add(int x, int y) {
            int iSum = x + y;
            if ((iSum % 5 == 0) && (OnMultipleOfFiveReached != null)) { OnMultipleOfFiveReached(this, new MultipleOfFiveEventArgs(iSum)); }
            if ((iSum % 10 == 0) && (OnMultipleOfTenReached != null)) { OnMultipleOfTenReached(this, new MultipleOfTenEventArgs(iSum)); }
            if ((iSum % 5 != 0 && iSum % 10 != 0) && (OnMultipleOfOtherReached != null)) { OnMultipleOfOtherReached(this, new MultipleOfOtherEventArgs(iSum)); }
            return iSum;
        }

        private static void show(MultipleOfEventArgs e) {
            Console.WriteLine($"{e.Value} is a multiple of {e.Text} reached! ");
        }

        public static void a_MultipleOfFiveReached(object sender, MultipleOfEventArgs e) { show(e); }

        public static void a_MultipleOfTenReached(object sender, MultipleOfTenEventArgs e) { show(e);}

        public static void a_MultipleOfOtherReached(object sender, MultipleOfOtherEventArgs e) { show(e); }
    }
}