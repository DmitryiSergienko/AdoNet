using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Lesson_3_CommandBuilder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string constr = ConfigurationManager.ConnectionStrings["Company_db"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // stp_CustomerAdd
                /*string cust_add_old = "dbo.stp_CustomerAdd";
                SqlCommand cmdOld = new SqlCommand(cust_add_old, conn);
                cmdOld.CommandType = System.Data.CommandType.StoredProcedure;
                cmdOld.Parameters.AddWithValue("@FirstName", "Ella");
                cmdOld.Parameters.AddWithValue("@LastName", "Chornogor");
                cmdOld.Parameters.AddWithValue("@DateOfBirth", DateTime.Now.ToShortDateString());
                SqlParameter cust_id = cmdOld.Parameters.Add("@CustomerID", System.Data.SqlDbType.Int);
                cust_id.Direction = ParameterDirection.Output; //Output параметр
                cmdOld.ExecuteNonQuery();
                Console.WriteLine((int)cust_id.Value);*/

                conn.Open();
                // with output
                string cust_add = "dbo.stp_CustomerAdd";
                SqlCommand cmd = new SqlCommand(cust_add, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cmd);
                cmd.Parameters[4].Value = DBNull.Value;
                cmd.Parameters[1].Value = "NeFN";
                cmd.Parameters[2].Value = "NeFN";
                cmd.Parameters[3].Value = DateTime.Now.AddYears(-1).ToShortDateString();
                cmd.ExecuteNonQuery();
                int new_id = (int)cmd.Parameters[4].Value;
                Console.WriteLine(new_id);

                // with return
                string cust_add2 = "dbo.stp_CustomerAdd_2";
                SqlCommand cmd2 = new SqlCommand(cust_add2, conn);
                cmd2.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cmd2);
                cmd2.Parameters[0].Value = DBNull.Value;
                cmd2.Parameters[1].Value = "NeFN_10";
                cmd2.Parameters[2].Value = "NeFN_10";
                cmd2.Parameters[3].Value = DateTime.Now.AddYears(-10).ToShortDateString();
                cmd2.ExecuteNonQuery();
                int new_id2 = (int)cmd2.Parameters[0].Value;
                Console.WriteLine(new_id2);
            }
        }
    }
}