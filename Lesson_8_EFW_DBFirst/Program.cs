using Lesson_8_EFW_DBFirst.DataLayer;
using Lesson_8_EFW_DBFirst.Models;
using System;
using System.Collections.Generic;

namespace Lesson_8_EFW_DBFirst
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = new BV425_CompanyDBEntities();

            // All
            Console.WriteLine("All");
            IEnumerable <CustomerModel> models = DL.Customer.All();
            foreach (CustomerModel model in models) 
            {
                Console.WriteLine(model);
            }
            Console.WriteLine("\n==================================================\n");

            // ByID
            Console.WriteLine("ByID");
            CustomerModel customerModel = DL.Customer.ByID(1004);
            Console.WriteLine(customerModel);
            Console.WriteLine("\n==================================================\n");

            // Insert
            Console.WriteLine("Insert");
            var newCust = new CustomerModel(1234, "NEW_FN", "NEW_LN", new DateTime(2022, 1, 10));
            int customerNewID = DL.Customer.Insert(newCust);
            Console.WriteLine(customerNewID);
        }
    }
}