using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07052025
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mammal m1 = new Mammal("milka", 3, "Krava", 250);
            Mammal m2 = new Mammal("redbul", 3, "Bik", 400);

            Bird b1 = new Bird("Mordecai", 19, "Blue Jay", 2.4);
            Bird b2 = new Bird("birb", 2, "crow", 4120);

            Reptile r1 = new Reptile("Snek", 4, "Zmiq", 4);
            Reptile r2 = new Reptile("Wizard", 3, "Gushter", 6);

            Employee e1 = new Employee("Alex", 26, m1, b1, b2);
            Employee e2 = new Employee("Viktor", 43);
            e2.AddAnimal(m2);
            e2.AddAnimal(r1);
            e2.AddAnimal(r2);

            Zoo z = new Zoo("ZOOOO", e1, e2);

            foreach(var e in z.Employees)
            {
                Console.WriteLine();
                e.ShowAssignedAnimals();
                Console.WriteLine();
            }

        }
    }

    class Zoo
    {
        public string Name { get; private set; }
        public List<Employee> Employees { get; private set; }

        public Zoo(string name, params Employee[] employees)
        {
            Name = name;
            Employees = new List<Employee>();
            Employees.AddRange(employees);
        }

        public void AddEmployee(Employee employee) => Employees.Add(employee);

        public void RemoveEmployee(Employee employee) => Employees.Remove(employee);
    }

    class Employee
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public List<Animal> AssignedAnimals { get; private set; }

        public Employee(string name, int age, params Animal[] assignedAnimals)
        {
            Name = name;
            Age = age;
            AssignedAnimals = new List<Animal>();
            AssignedAnimals.AddRange(assignedAnimals);
        }
        public Employee():this("", 0)
        {
            
        }

        public void AddAnimal(Animal animal)
        {
            if (!AssignedAnimals.Contains(animal))
                AssignedAnimals.Add(animal);
        }
        public void RemoveAnimal(Animal animal)
        {
            AssignedAnimals.Remove(animal);
        }

        public void RemoveAnimal(int ID)
        {
            Animal toRemove = AssignedAnimals.Where(x => x.ID == ID).FirstOrDefault();
            AssignedAnimals.Remove(toRemove);
        }

        public void ShowAssignedAnimals()
        {
            Console.WriteLine(this);
            Console.WriteLine("Animals under care:");
            foreach(var a in AssignedAnimals)
            {
                Console.WriteLine("\t" + a);
            }
        }

        public override string ToString()
        {

            string s = $"[Zookeeper's info]\n{Name} - {Age}";
            return s;
        }
    }

    class Animal
    {
        private static int _idCounter = 0;
        public int ID { get; protected set; }
        public string Name { get; protected set; }
        public int Age { get; protected set; }
        public string Type { get; protected set; }

        public Animal()
        {
            ID = _idCounter++;
        }
        public Animal(string name, int age, string type):this()
        {
            Name = name;
            Age = age;
            Type = type;
        }

        public override string ToString()
        {
            string s = $"[{ID}] {Name} {Age} years old";
            return s;
        }
    }

    class Mammal : Animal
    {
        public double Weight { get; protected set; }
        public Mammal():base()
        {
            
        }
        public Mammal(string name, int age, string type, double weight):
            base(name, age, type)
        {
            Weight = weight;
        }

        public override string ToString()
        {
            string s = $"[{ID}] <Mammal> /{Type}/ {Name} {Age} years old, weighs {Weight}kgs";
            return s;
        }
    }

    class Bird:Animal
    {
        public double FlightDuration { get; protected set; }
        public Bird() : base()
        {

        }
        public Bird(string name, int age, string type, double flightDuration):base(name, age, type)
        {
            FlightDuration = flightDuration;
        }

        public override string ToString()
        {
            string s = $"[{ID}] <Bird> /{Type}/ {Name} {Age} years old, flies for {FlightDuration}";
            return s;
        }
    }

    class Reptile : Animal
    {
        public double BloodTemp { get; protected set; }
        public Reptile() : base()
        {

        }
        public Reptile(string name, int age, string type, double bloodTemp): base(name, age, type)
        {
            BloodTemp = bloodTemp;
        }

        public override string ToString()
        {
            string s = $"[{ID}] <Reptile> /{Type}/ {Name} {Age} years old, Blood Temp is {BloodTemp}°C";
            return s;
        }
    }
}
