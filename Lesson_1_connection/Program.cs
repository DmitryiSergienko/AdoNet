/*
    -C#
    -SQL
    -: Linq to Object
    -: Linq to SQL
    -: Linq to XML
    -WP/WPF
    Net.Framework - среда разработки ПО для приложений в ОС Windows Net.6
    Net.Core -> .Net - кроссплатформенная
    DOT.NET -> ADO.NET - часть Framework
    Провайдер

    1. Connection - Соединение с БД
    2. Query - запросы, команды (View, Function(), хранимые процедуры, триггеры)
    3. Read query data - чтение данных
    4. Non connection - отсоединиться от БД

    Режим работы: присоединенный//отсоединенный

    Классы:
    * DbConnection;
    * DbCommand;    ExecuteReader() -> table, ExecuteNonQuery() -> int, ExecuteScalar() -> функция агрегирования
    * DbDataReader;
    * DbDataAdapter;

    * DataTable;
    * DataSet.

    Режимы работы с базами:
    * DBFirst;
    * ModelFirst;
    * CodeFirst;

    * DataLayer;
    * Linq_to_SQL;
    * Async.
 */

using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Lesson_1_connection
{
    internal class Program
    {
        enum len
        {
            len5 = 5,
            len10 = 10
        }

        void query(string sqlcommand, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(sqlcommand, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    var text = dr[i].ToString();
                    if (text.Length == 0)
                    {
                        text = "NULL";
                    }

                    if ((int)len.len5 > text.ToString().Length)
                    {
                        Console.Write($"{text,(int)len.len5}\t");
                    }
                    else
                    {
                        Console.Write($"{text,(int)len.len10}\t");
                    }
                }
                Console.WriteLine();
            }
            dr.Close();
            Console.WriteLine("\n===================================================================================================\n");
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            // Ia
            // a
            //string connect = "Server=(localdb)\\MSSQLLocalDB;Database=BV425_CompanyDB;User Id=user1;Password=sa;\r\n";
            //SqlConnection conn = new SqlConnection(connect);
            //conn.ConnectionString = connect;

            // c
            //SqlConnection conn = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=BV425_CompanyDB;User Id=user1;Password=sa;\r\n");

            // b
            //string connect = "Server=(localdb)\\MSSQLLocalDB;Database=BV425_CompanyDB;User Id=user1;Password=sa;\r\n";
            //SqlConnection conn = new SqlConnection(connect);
            

            // Ib
            string constr = ConfigurationManager.ConnectionStrings["Company_db"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();

                // Код работы с БД
                {
                    Console.WriteLine("Отображение таблицы (SELECT)\n");

                    string sqlcommand = "SELECT * FROM [BV425_CompanyDB].[dbo].[Employee]";
                    SqlCommand cmd = new SqlCommand(sqlcommand, conn);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        //var f0 = dr["EmployeeID"];
                        //var f2 = dr["LastName"];
                        //var f5 = dr["Salary"];

                        Console.WriteLine($"{dr["EmployeeID"],4}{dr["LastName"],15}{dr["Salary"],10}");
                    }
                    dr.Close();
                    Console.WriteLine("\n===================================================================================================\n");

                    //2 ExecuteScalar()
                    string sqlcom2 = "select sum(Salary) as SalaryTotal from Employee";
                    SqlCommand cmd2 = new SqlCommand(sqlcom2, conn);
                    object res = cmd2.ExecuteScalar();
                    Console.WriteLine($"Salary total = {res}");
                    Console.WriteLine("\n===================================================================================================\n");

                    //3 ExecuteNonQuery()
                    string sqlcom3 = "insert into Position (PositionName) values('Director')";
                    SqlCommand cmd3 = new SqlCommand(sqlcom3, conn);
                    int res2 = cmd3.ExecuteNonQuery();
                    if (res2 == 1)
                    {
                        Console.WriteLine($"OK {res}");
                    }
                    else
                    {
                        Console.WriteLine(res);
                    }
                    Console.WriteLine("\n===================================================================================================\n");
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    Console.WriteLine("Сортировка (ORDER BY)\n");

                    sqlcommand = @"
                                SELECT * FROM [BV425_CompanyDB].[dbo].[Employee]
                                ORDER BY Salary DESC
                                ";
                    program.query(sqlcommand, conn);

                    Console.WriteLine("Группировка и агрегационные фукнции (GROUP BY, COUNT)\n");

                    sqlcommand = @"
                                SELECT FirstName, COUNT(*) AS Count FROM [BV425_CompanyDB].[dbo].[Employee]
                                GROUP BY FirstName
                                ";
                    program.query(sqlcommand, conn);

                    Console.WriteLine("Условие с ограничением (WHERE >, <, =)\n");

                    sqlcommand = @"
                                SELECT * FROM [BV425_CompanyDB].[dbo].[Employee]
                                WHERE Salary > 1500
                                ";
                    program.query(sqlcommand, conn);

                    Console.WriteLine("Условие с совпадением по значению (WHERE LIKE)\n");

                    sqlcommand = @"
                                SELECT * FROM [BV425_CompanyDB].[dbo].[Employee]
                                WHERE FirstName LIKE '%va%'
                                ";
                    program.query(sqlcommand, conn);

                    Console.WriteLine("Присоединение одной таблицы к другой (JOIN)\n");

                    sqlcommand = @"
                                SELECT Customers.Id, Customers.FirstName, Pictures.Id FROM [BV425_CompanyDB].[dbo].[Pictures]
                                RIGHT JOIN Customers ON Pictures.Customer_ID = Customers.id
                                ";
                    program.query(sqlcommand, conn);
                }
            }
        }
    }
}