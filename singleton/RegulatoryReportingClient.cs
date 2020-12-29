using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace crosstraining.singleton {
    public static class RegulatoryReportingClient {
        private static DateTime StartTime;

        static RegulatoryReportingClient() {
            StartTime = DateTime.UtcNow;
        }

        public static void SendAsync() {
            Console.WriteLine(StartTime.Ticks);

            ////int polling499 = 499;
            ////int polling500 = 500;
            ////int polling501 = 501;
            ////int polling1000 = 1000;
            ////int polling1001 = 1001;
            ////int millSeg = 1000;

            ////Console.WriteLine((polling499 + millSeg - 0) / millSeg);
            ////Console.WriteLine((polling500 + millSeg - 0) / millSeg);
            ////Console.WriteLine((polling501 + millSeg - 0) / millSeg);
            ////Console.WriteLine((polling1000 + millSeg - 0) / millSeg);
            ////Console.WriteLine((polling1001 + millSeg - 0) / millSeg);

            Console.WriteLine((int) Math.Ceiling(((double) 500) / 1000));
            Console.WriteLine((int) Math.Ceiling(((double) 999) / 1000));
            Console.WriteLine((int) Math.Ceiling(((double) 1000) / 1000));
            Console.WriteLine((int) Math.Ceiling(((double) 2000) / 1000));
            ////Console.WriteLine(Math.Ceiling((decimal)(polling / 1000)));
            ////Console.WriteLine(Math.Round((decimal)(polling / 1000)));
            ////Console.WriteLine(Math.Floor((decimal)(polling / 1000)));
        }
    }
}