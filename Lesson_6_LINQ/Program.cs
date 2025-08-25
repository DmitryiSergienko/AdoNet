using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Lesson_6_LINQ
{
    internal class Program
    {
        // Виды LINQ:
        // Linq to Object
        // Linq to SQL
        // Lint to XML
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();
            Create_List_Student(students);
            foreach (Student student in students)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine("====================================");

            Find_linq_student(students);
        }

        public static void Create_List_Student(List<Student> students)
        {
            Random rnd = new Random();
            string[] city = { "c2", "c4", "c5", "c10", "c1", "c123", "c45" };

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(200);
                students.Add(new Student { FN = "N" + i, LN = "LN" + i, City = city[rnd.Next(0, city.Length - 1)], Age = rnd.Next(16, 20) });
            }
        }
        public static void Find_linq_student(List<Student> students)
        {
            var res = from s in students
                      where s.Age >= 18
                      orderby s.Age
                      select new { s.LN, s.Age };

            foreach (var r in res)
            {
                Console.WriteLine(r);
            }
        }
    }
}