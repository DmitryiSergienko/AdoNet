using Lesson_7_linq_to_SQL.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;

namespace Lesson_7_linq_to_SQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string constr = ConfigurationManager.ConnectionStrings["Company_db"].ConnectionString;
            using (DataContext dc = new DataContext(constr))
            {
                // All customer
                Console.WriteLine("All customer:");
                Table<Customer> customers = dc.GetTable<Customer>();
                foreach (Customer customer in customers)
                {
                    Console.WriteLine(customer);
                }
                Console.WriteLine("====================================================\n");

                // Top 2
                Console.WriteLine("Top 2:");
                List<Customer> res = customers.Take(2).ToList();
                foreach (Customer customer in res)
                {
                    Console.WriteLine(customer);
                }
                Console.WriteLine("====================================================\n");

                // Customer by id
                Console.WriteLine("Customer by id:");
                var query = from customer in customers
                            where customer.id == 3
                            select customer;
                foreach (Customer customer in query)
                {
                    Console.WriteLine(customer);
                }
                Console.WriteLine("====================================================\n");

                // Customer year >= 2015
                Console.WriteLine("Customer year >= 2015 && year < 2025:");
                var queryYear = from customer in customers 
                                where customer.DateOfBirth.Year >= 2015
                                && customer.DateOfBirth.Year < 2025
                                select customer;
                foreach (Customer customer in queryYear)
                {
                    Console.WriteLine(customer);
                }
                Console.WriteLine("====================================================\n");

                // Customer FN -> I
                Console.WriteLine("Customer FN -> I:");
                var queryStartNameI = from customer in customers 
                                      where customer.FirstName.StartsWith("I") 
                                      select customer;
                foreach (Customer customer in queryStartNameI)
                {
                    Console.WriteLine(customer);
                }
                Console.WriteLine("====================================================\n");

                // Customer Insert
                //Console.WriteLine("Customer Insert:");
                //Customer cust_new = new Customer 
                //{
                //    DateOfBirth = new DateTime(1999, 11, 1),
                //    LastName = "LN_new_customer",
                //    FirstName = "FN_new_customer"
                //};
                //customers.InsertOnSubmit(cust_new);
                //dc.SubmitChanges();

                //foreach (Customer customer in customers)
                //{
                //    Console.WriteLine(customer);
                //}
                //Console.WriteLine("====================================================\n");
                
                // Customer Edit
                Console.WriteLine("Customer Insert:");
                Customer c_edit = customers.Where(c => c.id == 7004).First();
                c_edit.LastName += "_redacted";
                Console.WriteLine(c_edit);
                dc.SubmitChanges();
                Console.WriteLine("+++++++++++++++++++++++++++++++++++");
                foreach (Customer item in customers)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("====================================================\n");
            }
        }
    }
}