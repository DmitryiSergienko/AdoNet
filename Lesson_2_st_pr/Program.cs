using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Lesson_2_st_pr
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string constr = ConfigurationManager.ConnectionStrings["Company_db"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();

                // stp_CustomerAll
                string custAll = "dbo.stp_CustomerAll";
                SqlCommand cmd = new SqlCommand(custAll, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine($"{dr["id"],4}{dr["LastName"],15}{dr["DateOfBirth"],10}"); //ToShortDateString
                }
                dr.Close();
                Console.WriteLine("\n=============================================================================================\n");

                // stp_CustomerAdd
                string cust_add = "dbo.stp_CustomerAdd";
                SqlCommand cmd2 = new SqlCommand(cust_add, conn);
                cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@FirstName", "Ella");
                cmd2.Parameters.AddWithValue("@LastName", "Chornogor");
                cmd2.Parameters.AddWithValue("@DateOfBirth", DateTime.Now.ToShortDateString());
                SqlParameter cust_id = cmd2.Parameters.Add("@CustomerID", System.Data.SqlDbType.Int);
                cust_id.Direction = ParameterDirection.Output; //Output параметр
                cmd2.ExecuteNonQuery();
                Console.WriteLine((int)cust_id.Value);
            }
        }
    }
}