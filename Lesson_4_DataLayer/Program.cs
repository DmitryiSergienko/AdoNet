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

            int id = DL.Customer.Insert(new CustomerModel(0, "FN_new", "LN_new", new DateTime(2024, 3, 15)));
            Console.WriteLine(id);

            List<CustomerModel> allCustomers = DL.Customer.All();
            foreach (CustomerModel customer in allCustomers)
            {
                Console.WriteLine(customer.ToString());
            }
        }
    }
}