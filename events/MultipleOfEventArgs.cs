namespace crosstraining.events {
    using System;

    public abstract class MultipleOfEventArgs : EventArgs {
        public MultipleOfEventArgs(int value) { Value = value; }
        public MultipleOfEventArgs(int value, string text) { Value = value; Text = text; }
        public int Value { get; set; }
        public string Text { get; set; }
    }
    public class MultipleOfFiveEventArgs : MultipleOfEventArgs {
        public MultipleOfFiveEventArgs(int value) : base(value) {
            this.Text = "FIVE";
        }
    }
    public class MultipleOfTenEventArgs : MultipleOfEventArgs {
        public MultipleOfTenEventArgs(int value) : base(value) {
            this.Text = "TEN";
        }
    }
    public class MultipleOfOtherEventArgs : MultipleOfEventArgs {
        public MultipleOfOtherEventArgs(int value) : base(value, "OTHER") {}
    }
}