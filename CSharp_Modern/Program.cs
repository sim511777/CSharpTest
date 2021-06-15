using System;
using static System.Console;

namespace CSharp_Modern {
    static class Program {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main() {
        }
    }
}

//int FibOld(int i) {
//    if (i <= 2) {
//        return 1;
//    } else {
//        return FibOld(i - 2) + FibOld(i - 1);
//    }
//}

//int Fib(int i) =>
//    i switch {
//        int when i <= 2 => 1,
//        _ => Fib(i + 2) + Fib(i - 1),
//    };

//WriteLine($"20번째: {Fib(20)}");
//WriteLine($"5번째: {Fib(5)}");