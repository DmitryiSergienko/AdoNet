using Lesson_10_CodeFirst.Models;
using System;

namespace Lesson_10_CodeFirst
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new CompanyDBContext())
            {
                // Очищаем таблицы
                db.Customers.RemoveRange(db.Customers);
                db.Pictures.RemoveRange(db.Pictures);
                db.Employees.RemoveRange(db.Employees);
                db.Positions.RemoveRange(db.Positions);

                db.SaveChanges(); // Сохраняем удаление

                // =============== КЛИЕНТ + КАРТИНКА ===============
                Customer new_cust = new Customer();
                new_cust.FirstName = "FN";
                new_cust.LastName = "LN";
                new_cust.DateOfBirth = new DateTime(2000, 01, 01);
                db.Customers.Add(new_cust);
                db.SaveChanges();

                Picture new_pict = new Picture();
                new_pict.Name_pict = "Pic";
                new_pict.CustomerID = new_cust.Id;
                db.Pictures.Add(new_pict);
                db.SaveChanges();

                // =============== ДОЛЖНОСТЬ + СОТРУДНИКИ ===============
                // Сначала добавляем должность
                Position position = new Position();
                position.Position_name = "Менеджер";
                db.Positions.Add(position);
                db.SaveChanges();

                // Первый сотрудник
                Employee emp1 = new Employee();
                emp1.FirstName = "Иван";
                emp1.LastName = "Иванов";
                emp1.Salary = 50000;
                emp1.DateOfBirth = new DateTime(1990, 5, 15);
                emp1.PositionId = position.Id;
                db.Employees.Add(emp1);

                // Второй сотрудник
                Employee emp2 = new Employee();
                emp2.FirstName = "Петр";
                emp2.LastName = "Петров";
                emp2.Salary = 60000;
                emp2.DateOfBirth = new DateTime(1985, 8, 22);
                emp2.PositionId = position.Id;
                db.Employees.Add(emp2);

                db.SaveChanges();
            }
        }
    }
}