using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _23042025
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Ex1();
            Ex2();

        }
        static void Ex1()
        {
            List<Student> students = new List<Student>();

            //SeedStudentList(students);
            //StudentsListToCSV(students);

            CSVToStudentsList(students);
            ViewList(students);
        }
        static void ViewList<T>(List<T> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
        static void SeedStudentList(List<Student> students)
        {
            students.Add(new Student("Ivan", 19, 6));
            students.Add(new Student("Petur", 18, 5));
            students.Add(new Student("Kaloqn", 19, 3));
            students.Add(new Student("Joro", 19, 4.5));
            students.Add(new Student("Lubo", 19, 5.44));
        }

        static void StudentsListToCSV(List<Student> students)
        {
            string fileName = "data.txt";

            FileStream fs = new FileStream(fileName, FileMode.Create);

            using (StreamWriter writer = new StreamWriter(fs))
            {
                foreach(Student s in students)
                {
                    writer.WriteLine(s.ToString());
                }
                fs.Flush();
            }
            fs.Dispose();
        }

        static void CSVToStudentsList(List<Student> students)
        {
            string fileName = "data.txt";

            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    students.Add(new Student(data[0], int.Parse(data[1]), double.Parse(data[2])));
                }
            }
        }


        static void Ex2()
        {
            #region path
            string path = @"C:\Users\Freez_\Desktop\Software Engineering\PU\I kurs\OOP 2 - C#\23042025\23042025\keywords.txt";
            #endregion
            string keyword = Console.ReadLine();
            var matches = FindMatches(keyword, path);
            ViewList(matches);

        }

        static List<string> FindMatches(string keyword, string path)
        {

            List<string> list = new List<string>();

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                int lineIndex = 1;
                while((line = sr.ReadLine()) != null)
                {
                    string[] words = line.Split(' ', ',', '.', '?', '!');
                    
                    foreach(string w in words)
                    {
                        if (w.StartsWith(keyword))
                        {
                            list.Add($"[{lineIndex}] - \"{w}\"");
                        }
                    }
                    lineIndex++;
                }
            }
            

            return list;
        }

    }

    class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public double Grade { get; set; }

        public Student(string name, int age, double grade)
        {
            Name = name;
            Age = age;
            Grade = grade;
        }
        public Student() : this("", 0, 0){}

        public override string ToString()
        {
            return $"{Name},{Age},{Grade}";
        }
    }
}
