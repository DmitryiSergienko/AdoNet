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
                cmd2.Parameters.AddWithValue("@DateOfBirth", DateTime.Today); //ToShortDateString не всегда работает
                SqlParameter cust_id = cmd2.Parameters.Add("@CustomerID", System.Data.SqlDbType.Int);
                cust_id.Direction = ParameterDirection.Output; //Output параметр
                cmd2.ExecuteNonQuery();
                Console.WriteLine((int)cust_id.Value);

                // stp_EmployeeByID
                string command = "[dbo].[stp_EmployeeByID]";
                SqlCommand cmd3 = new SqlCommand(command, conn);
                cmd3.CommandType = System.Data.CommandType.StoredProcedure;

                // 1
                //SqlParameter empl_id = cmd3.Parameters.Add("@employeeID", System.Data.SqlDbType.Int);
                //empl_id.Value = 4;

                // 2
                cmd3.Parameters.AddWithValue("@employeeID", 4);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                while (dr3.Read()) 
                {
                    var f0 = dr3[0];
                    var f2 = dr3[2];
                    var f5 = dr3[5];
                    Console.WriteLine($"{f0,-4}{f2,15}{f5,10}");
                }
                dr3.Close();

                // delete
                string command4 = "[dbo].[stp.CustomerDelete]";
                SqlCommand cmd4 = new SqlCommand(command4, conn);
                cmd4.CommandType = System.Data.CommandType.StoredProcedure;
                cmd4.Parameters.AddWithValue("@customerID", 4);
                int rowsAffected = cmd4.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Объект удален");
                }
                else 
                {
                    Console.WriteLine("Ошибка при удалении");
                }
            }
        }
    }
}