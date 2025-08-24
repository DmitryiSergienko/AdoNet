using Lesson_4_DataLayer.DataLayer;
using Lesson_4_DataLayer.Models;
using System;
using System.Collections.Generic;

namespace Lesson_4_DataLayer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CustomerModel cust1 = DL.Customer.ByID(1);
            CustomerModel cust2 = DL.Customer.ByID(2);
            Console.WriteLine(cust1);
            Console.WriteLine(cust2);

            var cust3 = new CustomerModel(0, "FN_new", "LN_new", new DateTime(2024, 3, 15));
            int id = DL.Customer.Insert(cust3);
            cust3.ID = id;
            Console.WriteLine(id);

            List<CustomerModel> allCustomers = DL.Customer.All();
            foreach (CustomerModel customer in allCustomers)
            {
                Console.WriteLine(customer.ToString());
            }

            bool res = DL.Customer.Delete(cust3.ID);
            if (res) Console.WriteLine($"Пользователь c id '{id}' удален!");
            else Console.WriteLine("Ошибка при удалении пользователя!");

            id = DL.Customer.Insert(cust3);
            cust3.ID = id;
            var new_cust = new CustomerModel(
                cust3.ID,
                "new_Name",
                "new_Surname",
                new DateTime(2022, 1, 10)
                );
            res = DL.Customer.Update(new_cust);
            if (res)
            {
                Console.WriteLine("Обновлен пользователь:");
                Console.WriteLine(new_cust + "\n");
            }
            else Console.WriteLine("Ошибка при обновлении пользователя!");
            DL.Customer.Delete(new_cust.ID);
        }
    }
}