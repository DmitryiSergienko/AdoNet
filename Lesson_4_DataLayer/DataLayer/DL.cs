using Lesson_4_DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Lesson_4_DataLayer.DataLayer
{
    public class DL
    {
        public static string ConnectionString { get; private set; } = ConfigurationManager.ConnectionStrings["Company_db"].ConnectionString;
        static SqlConnection conn;

        public static class Customer
        {
            public static CustomerModel ByID(int customerId)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand getCustomer = new SqlCommand("stp_CustomerByID", conn);
                    getCustomer.Parameters.AddWithValue("@customerID", customerId);
                    getCustomer.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = getCustomer.ExecuteReader();
                    CustomerModel customer = null;
                    while (reader.Read())
                    {
                        int ID = (int)reader[0];
                        string FirstName = reader[1].ToString();
                        string LastName = reader[2].ToString();
                        DateTime birthDate = DateTime.Parse(reader[3].ToString());
                        customer = new CustomerModel(ID, FirstName, LastName, birthDate);
                    }
                    reader.Close();
                    return customer;
                }
            }
            public static int Insert(CustomerModel tmp)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string cust_add = "dbo.stp_CustomerAdd";
                    SqlCommand cmd = new SqlCommand(cust_add, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlCommandBuilder.DeriveParameters(cmd);
                    cmd.Parameters[4].Value = DBNull.Value;
                    cmd.Parameters[1].Value = tmp.FirstName;
                    cmd.Parameters[2].Value = tmp.LastName;
                    cmd.Parameters[3].Value = tmp.BirthDate;
                    cmd.ExecuteNonQuery();
                    int new_id = (int)cmd.Parameters[4].Value;
                    return new_id;
                }
            }
            public static List<CustomerModel> All()
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string custAll = "dbo.stp_CustomerAll";
                    SqlCommand cmd = new SqlCommand(custAll, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dr = cmd.ExecuteReader();
                    List <CustomerModel> allCustomers = new List <CustomerModel>();
                    while (dr.Read())
                    {
                        int ID = (int)dr[0];
                        string FirstName = dr[1].ToString();
                        string LastName = dr[2].ToString();
                        DateTime birthDate = DateTime.Parse(dr[3].ToString());
                        CustomerModel customer = new CustomerModel(ID, FirstName, LastName, birthDate);
                        allCustomers.Add(customer);
                    }
                    dr.Close();
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

                    cmd.Parameters.AddWithValue("@customerID", customer.ID);
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", customer.BirthDate);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}