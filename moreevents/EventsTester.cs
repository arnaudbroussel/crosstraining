using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crosstraining.moreevents
{
    internal class EventsTester
    {
        public static void Example1()
        {
            var threshold = new Random().Next(10)+1;
            Counter1 c = new Counter1(threshold);
            c.ThresholdReached += c_ThresholdReached1;

            Console.WriteLine($"press 'a' key to increase total until {threshold} times");
            while (Console.ReadKey(true).KeyChar == 'a')
            {
                Console.WriteLine("adding one");
                c.Add(1);
            }
        }

        public static void Example2()
        {
            var threshold = new Random().Next(10) + 1;
            Counter2 c = new Counter2(threshold);
            c.ThresholdReached += c_ThresholdReached2;

            Console.WriteLine($"press 'a' key to increase total until {threshold} times");
            while (Console.ReadKey(true).KeyChar == 'a')
            {
                Console.WriteLine("adding one");
                c.Add(1);
            }
        }
        
        public static void Example3()
        {
            var threshold = new Random().Next(10) + 1;
            Counter3 c = new Counter3(threshold);
            c.ThresholdReached += c_ThresholdReached3;

            Console.WriteLine($"press 'a' key to increase total until {threshold} times");
            while (Console.ReadKey(true).KeyChar == 'a')
            {
                Console.WriteLine("adding one");
                c.Add(1);
            }
        }

        private static void c_ThresholdReached1(object sender, EventArgs e)
        {
            Console.WriteLine("The threshold was reached.");
        }

        private static void c_ThresholdReached2(object sender, ThresholdReachedEventArgs e)
        {
            Console.WriteLine("The threshold of {0} was reached at {1}.", e.Threshold, e.TimeReached);
            Environment.Exit(0);
        }

        static void c_ThresholdReached3(Object sender, ThresholdReachedEventArgs e)
        {
            Console.WriteLine("The threshold of {0} was reached at {1}.", e.Threshold, e.TimeReached);
            Environment.Exit(0);
        }
    }
}
