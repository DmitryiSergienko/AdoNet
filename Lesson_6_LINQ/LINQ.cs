using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Lesson_6_LINQ
{
    public class LINQ
    {
        public LINQ() {}
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
        {   // LINQ to object
            var res = from s in students
                      where s.Age >= 18
                      orderby s.Age
                      select new { s.LN, s.Age };

            foreach (var r in res)
            {
                Console.WriteLine(r);
            }
        }
        public static void Avg_age_linq(List<Student> students)
        {   // Лямбда выражение
            var avg_age = students.Select(s => s.Age).Average();
            Console.WriteLine(avg_age);
        }
        public static void Min_age_linq(List<Student> students)
        {   // Лямбда выражение
            int min_age = students.Min(s => s.Age);
            Console.WriteLine(min_age);
        }
        public static void Min_age_linq_1(List<Student> students)
        {   // Лямбда выражение
            int min_age = students.Min(s => s.Age);
            Console.WriteLine(min_age);

            var st_tmp = students.First(s => s.Age == min_age);
            Console.WriteLine(st_tmp);
        }
        public static void Min_age_linq_2(List<Student> students)
        {   // Лямбда выражение
            int min_age = students.Min(s => s.Age);
            Console.WriteLine(min_age);

            var res = from student in students
                      where student.Age > min_age
                      orderby student.Age
                      select student;

            foreach (var r in res)
            {
                Console.WriteLine(r);
            }
        }
        public static void Show_Color_More_3_L_1()
        {
            string[] color = { "red", "green", "blue", "red", "yellow", "black", "blue", "brown", "green", "yellow" };
            var res = from c in color
                      where c.Length > 3
                      select c;
            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
        }
        public static void Show_Color_More_3_L_2()
        {
            string[] color = { "red", "green", "blue", "red", "yellow", "black", "blue", "brown", "green", "yellow" };
            var res = (from c in color
                       where c.Length > 3
                       orderby c.Length descending //asc desc
                       select c).Distinct();
            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
        }
    }
}