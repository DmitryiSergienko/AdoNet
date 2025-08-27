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
            Console.WriteLine("Все студенты:");
            List<Student> students = new List<Student>();
            LINQ.Create_List_Student(students);
            foreach (Student student in students)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine("====================================");

            Console.WriteLine("\nПоиск студентов с определенным условием:");
            LINQ.Find_linq_student(students);
            Console.WriteLine("====================================");

            Console.WriteLine("\nСредний возраст студентов:");
            LINQ.Avg_age_linq(students);
            Console.WriteLine("====================================");

            Console.WriteLine("\nМинимальный возраст студентов:");
            LINQ.Min_age_linq(students);
            Console.WriteLine("====================================");

            Console.WriteLine("\nМинимальный возраст студентов:");
            LINQ.Min_age_linq_1(students);
            Console.WriteLine("====================================");

            Console.WriteLine("\nСтуденты больше минимального возраста:");
            LINQ.Min_age_linq_2(students);
            Console.WriteLine("====================================");

            Console.WriteLine("\nПоказать цвета у которых название больше 3 символов и отсортировать:");
            LINQ.Show_Color_More_3_L_1();
            Console.WriteLine("====================================");

            Console.WriteLine("\nПоказать цвета у которых название больше 3 символов, убрать дубли и отсортировать:");
            LINQ.Show_Color_More_3_L_2();
            Console.WriteLine("====================================");
        }
    }
}