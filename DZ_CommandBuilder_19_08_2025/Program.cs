using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DZ_CommandBuilder_19_08_2025
{
    public class Program
    {
        public SqlCommand CMD { set; get; }
        static void Main(string[] args)
        {
            var program = new Program();
            string constr = ConfigurationManager.ConnectionStrings["TZ_21_07_2025"].ConnectionString;
            SqlCommand cmd = program.CMD;
            string sqlcommand;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                
                // Предварительно удаляю созданных пользователей
                program.DeleteUsersWhereIdMore7(conn);

                // add_user
                Console.WriteLine("\t\t\t\tadd_user\n");
                string addUser = "dbo.add_user";
                cmd = new SqlCommand(addUser, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cmd);
                cmd.Parameters[1].Value = "newuser";
                cmd.Parameters[2].Value = "12345678";
                cmd.Parameters[3].Value = "Федор";
                cmd.Parameters[4].Value = "Федоров";
                cmd.Parameters[5].Value = "Федорович";
                cmd.Parameters[6].Value = "newuser@mail.ru";
                cmd.Parameters[7].Value = "+79428328282";
                cmd.ExecuteNonQuery();

                sqlcommand = @"SELECT id, login, name, surname FROM users";
                program.query(new SqlCommand(sqlcommand, conn), 3);

                // show_products_in_category
                Console.WriteLine("\t\t\t\tshow_products_in_category\n");
                string showProd = "dbo.show_products_in_category";
                cmd = new SqlCommand(showProd, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cmd);
                cmd.Parameters[1].Value = 5;
                program.query(cmd, 3);

                // search_orders_by_date
                Console.WriteLine("\t\t\t\tsearch_orders_by_date\n");
                string searchOrd = "dbo.search_orders_by_date";
                cmd = new SqlCommand(searchOrd, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cmd);
                cmd.Parameters["@start_date"].Value = new DateTime(2025, 1, 12, 0, 0, 0);
                cmd.Parameters["@end_date"].Value = new DateTime(2025, 1, 16, 23, 59, 59);
                program.query(cmd, 3);

                // search_products_by_name
                Console.WriteLine("\t\t\t\tsearch_products_by_name\n");
                string searchProd = "dbo.search_products_by_name";
                cmd = new SqlCommand(searchProd, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cmd);
                cmd.Parameters[1].Value = "Книга";
                program.query(cmd, 3);
            }
        }

        void query(SqlCommand cmd, int collums)
        {
            SqlDataReader dr = cmd.ExecuteReader();
            if (collums > dr.FieldCount)
            {
                collums = dr.FieldCount;
            }

            while (dr.Read())
            {
                for (int i = 0; i < collums; i++)
                {
                    var text = dr[i].ToString();
                    if (text.Length == 0)
                    {
                        text = "NULL";
                    }
                    Console.Write($"{text,20}\t");
                }
                Console.WriteLine();
            }
            dr.Close();
            Console.WriteLine("\n===================================================================================================\n");
        }
        public void DeleteUsersWhereIdMore7(SqlConnection conn)
        {
            string sqlcommand = "DELETE FROM users WHERE id >= @minId";
            CMD = new SqlCommand(sqlcommand, conn);
            CMD.Parameters.AddWithValue("@minId", 7);
            int count = CMD.ExecuteNonQuery();
        }
    }
}