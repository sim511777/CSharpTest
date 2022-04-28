using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest {
    public class Util {
        private static double Freq = (double)Stopwatch.Frequency;
        public static double GetTimeMs() {
            long now = Stopwatch.GetTimestamp();
            return (double)now / Freq * 1000.0;
        }
    }
}
