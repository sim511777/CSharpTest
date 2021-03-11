using static System.Console;

int FibOld(int i) {
    if (i <= 2) {
        return 1;
    } else {
        return FibOld(i - 2) + FibOld(i - 1);
    }
}

int Fib(int i) =>
    i switch {
        int when i <= 2 => 1,
        _ => Fib(i + 2) + Fib(i - 1),
    };

WriteLine($"20번째: {Fib(20)}");
WriteLine($"5번째: {Fib(5)}");