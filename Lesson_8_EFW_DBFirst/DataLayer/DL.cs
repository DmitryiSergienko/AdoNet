using Lesson_8_EFW_DBFirst.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Lesson_8_EFW_DBFirst.DataLayer
{
    public class DL
    {
        public static string ConnectionString { get; private set; } = ConfigurationManager.ConnectionStrings["Company_db"].ConnectionString;
        static SqlConnection conn;

        public static class Customer
        {
            public static CustomerModel ByID(int customerId)
            {
                using (var db = new BV425_CompanyDBEntities())
                {
                    CustomerModel tmp = new CustomerModel();
                    var res = db.stp_CustomerByID(customerId).First();
                    tmp.id = res.id;
                    tmp.FirstName = res.FirstName;
                    tmp.LastName = res.LastName;
                    tmp.DateOfBirth = res.DateOfBirth;
                    return tmp;
                }
            }
            public static int Insert(CustomerModel tmp)
            {
                using (var db = new BV425_CompanyDBEntities())
                {
                    var customerID = new System.Data.Entity.Core.Objects.ObjectParameter("CustomerId", typeof(int));

                    var res = db.stp_CustomerAdd(
                        tmp.FirstName,
                        tmp.LastName,
                        tmp.DateOfBirth,
                        customerID);

                    int newId = (int)customerID.Value;
                    return newId;
                }
            }
            public static IEnumerable<CustomerModel> All()
            {
                using (var db = new BV425_CompanyDBEntities())
                {
                    List<CustomerModel> allCustomers = new List<CustomerModel>();
                    var res = db.stp_CustomerAll().ToList();
                    foreach (var item in res) 
                    {
                        CustomerModel tmp = new CustomerModel();
                        tmp.id = item.id;
                        tmp.FirstName = item.FirstName;
                        tmp.LastName = item.LastName;
                        tmp.DateOfBirth = item.DateOfBirth;
                        allCustomers.Add(tmp);
                    }
                    return allCustomers;
                }
            }
            public static bool Delete(int custId)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string command = "[dbo].[stp.CustomerDelete]";
                    SqlCommand cmd = new SqlCommand(command, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customerID", custId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            public static bool Update(CustomerModel customer)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("stp_customer_update", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@customerID", customer.id);
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}