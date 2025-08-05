using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DZ1_1_08_2025
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();
            string constr = ConfigurationManager.ConnectionStrings["TZ_21_07_2025"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();

                // Предварительно удаляю созданных пользователей
                string sqlcommand = "DELETE FROM users WHERE id >= @minId";
                SqlCommand cmd = new SqlCommand(sqlcommand, conn);
                cmd.Parameters.AddWithValue("@minId", 7);
                int count = cmd.ExecuteNonQuery();

                // add_user
                Console.WriteLine("\t\t\t\tadd_user\n");
                string addUser = "dbo.add_user";
                cmd = new SqlCommand(addUser, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@login", "newuser");
                cmd.Parameters.AddWithValue("@password", "12345678");
                cmd.Parameters.AddWithValue("@name", "Федор");
                cmd.Parameters.AddWithValue("@surname", "Федоров");
                cmd.Parameters.AddWithValue("@patronymic", "Федорович");
                cmd.Parameters.AddWithValue("@mail", "newuser@mail.ru");
                cmd.Parameters.AddWithValue("@phone_number", "+79428328282");
                cmd.ExecuteNonQuery();

                sqlcommand = @"SELECT id, login, name, surname FROM users";
                program.query(new SqlCommand(sqlcommand, conn), 3);

                // show_products_in_category
                Console.WriteLine("\t\t\t\tshow_products_in_category\n");
                string showProd = "dbo.show_products_in_category";
                cmd = new SqlCommand(showProd, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@category_id", 5);
                program.query(cmd, 3);

                // search_orders_by_date
                Console.WriteLine("\t\t\t\tsearch_orders_by_date\n");
                string searchOrd = "dbo.search_orders_by_date";
                cmd = new SqlCommand(searchOrd, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@start_date", "2025-01-12 00:00:00");
                cmd.Parameters.AddWithValue("@end_date", "2025-01-16 23:59:59");
                program.query(cmd, 3);

                // search_products_by_name
                Console.WriteLine("\t\t\t\tsearch_products_by_name\n");
                string searchProd = "dbo.search_products_by_name";
                cmd = new SqlCommand(searchProd, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", "Книга");
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
    }
}