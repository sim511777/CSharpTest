using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Variance {
    class Program {
        static void Main(string[] args) {
            // class assign은 covariance
            Animal animal = new Bird("bird");
            //Duck duck = new Bird("Bird");

            // class 배열은 covariance
            Animal[] animals = new Bird[10];
            //Duck[] ducks = new Bird[10];

            // delegate 리턴타입은 covariance
            DelAnimalVoid av = FuncBirdVoid;
            //DelDuckVoid dv = FuncBirdVoid;

            // delegate 파라미터타입은 contravariance
            //DelVoidAnimal va = FuncVoidBird;
            DelVoidDuck vd = FuncVoidBird;

            // .net 4.0 부터 Generic variance 지원(out, in 키워드)
            // IEnumerable<out T>
            IEnumerable<Animal> animalList = new List<Bird>();
            //IEnumerable<Duck> duckList = new List<Bird>();
        }

        public static Animal FuncAnimalVoid() {
            return new Animal("animal");
        }
        public static Bird FuncBirdVoid() {
            return new Bird("bird");
        }
        public static Duck FuncDuckVoid() {
            return new Duck("duck");
        }

        public static void FuncVoidAnimal(Animal animal) {
        }
        public static void FuncVoidBird(Bird bird) {
        }
        public static void FuncVoidDuck(Duck duck) {
        }
    }

    delegate Animal DelAnimalVoid();
    delegate Bird DelBirdVoid();
    delegate Duck DelDuckVoid();

    delegate void DelVoidAnimal(Animal animal);
    delegate void DelVoidBird(Bird bird);
    delegate void DelVoidDuck(Duck duck);

    class Animal {
        protected string name;
        public Animal(string name) => this.name = name;
        public void Move() {
            Console.WriteLine($"{name} is moving");
        }
    }

    class Bird : Animal {
        public Bird(string name) : base(name) { }
        public void Fly() {
            Console.WriteLine($"{name} is flying");
        }
    }

    class Duck : Bird {
        public Duck(string name) : base(name) { }
        public void Swim() {
            Console.WriteLine($"{name} is swimming");
        }
    }
}
