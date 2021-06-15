using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustTest {
    class Program {
        static void Main(string[] args) {
            Derived derived = new Derived();
            derived.Fly();
        }
    }

    class Base {
        public void Run() => Console.WriteLine("Base.Run()");
        public void Fly() => Console.WriteLine("Base.Fly()");
    }

    class Derived : Base {
        public void NewFly() => Console.WriteLine("Derived.Fly()");
        public new void Fly() => Console.WriteLine("Derived.Fly()");
    }
}
