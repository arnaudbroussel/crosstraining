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

        public void ProcessAction(int x, int y, Action<int, int> action) {
            action(x, y);            
        }

        public Action<int, int> SumActionFor10 = (x, y) => {
            x = x * 10;
            y++;
            Console.WriteLine(x + y);
        };

        public Action<int, int> SumActionFor20 = (x, y) => {
            x = x * 20;
            y++;
            Console.WriteLine(x + y);
        };

        public Action<int, int> SumActionForOther = (x, y) => {
            if (x > 10)
                Console.WriteLine("(x > 10)");
            else
                Console.WriteLine("(x <= 10)");
        };
    }
}