using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Lesson_5_Adapter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string constr = ConfigurationManager.ConnectionStrings["Company_db"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();

                // all Position - select

                SqlDataAdapter pos_all_adap = new SqlDataAdapter("select * from dbo.Position order by 2", conn);
                SqlCommandBuilder cmd_position = new SqlCommandBuilder(pos_all_adap);
                DataSet ds_p = new DataSet();
                pos_all_adap.Fill(ds_p, "Positions");
                DataTable dt_pos = ds_p.Tables["Positions"];

                foreach (DataRow item in dt_pos.Rows)
                {
                    Console.WriteLine($"{item[0],5} {item[1],20}");
                }
                Console.WriteLine("\n=============================================\n");

                // all_Customer_stp

                SqlDataAdapter cust_all_adap = new SqlDataAdapter();
                cust_all_adap.InsertCommand = new SqlCommand("dbo.stp_CustomerAll", conn);
                cust_all_adap.InsertCommand.CommandType = CommandType.StoredProcedure;
                cust_all_adap.SelectCommand = cust_all_adap.InsertCommand;
                SqlCommandBuilder md_builder = new SqlCommandBuilder(cust_all_adap);
                DataSet ds_c = new DataSet();
                cust_all_adap.Fill(ds_c, "Customers");
                DataTable dt_customer = ds_c.Tables["Customers"];

                foreach (DataRow item in dt_customer.Rows)
                {
                    Console.WriteLine($"{item[0],5} {item[2],20} {Convert.ToDateTime(item[3]).ToShortDateString(),15}");
                }
                Console.WriteLine("\n=============================================\n");

                // add_Customer_stp

                SqlDataAdapter customer_adapter = new SqlDataAdapter("select * from dbo.Customers", conn);
                SqlCommandBuilder cmd_customer = new SqlCommandBuilder(customer_adapter);
                DataSet ds = new DataSet();
                customer_adapter.Fill(ds, "Customers");
                DataTable dt = ds.Tables["Customers"];
                DataRow new_customer = dt.NewRow();
                new_customer[1] = "New_customer_fn";
                new_customer[2] = "New_customer_ln";
                new_customer[3] = DateTime.Today;
                dt.Rows.Add(new_customer);

                // update_DB

                customer_adapter.Update(ds, "Customers");
                dt.Clear();
                customer_adapter.Fill(ds, "Customers");

                foreach (DataRow item in dt.Rows)
                {
                    Console.WriteLine($"{item[0],5} {item[1],20} {item[2],20} {Convert.ToDateTime(item[3]).ToShortDateString(),15}");
                }
                Console.WriteLine("\n=============================================\n");

                // delete

                int id_for_delete = (int)dt.Rows[dt.Rows.Count - 1][0];

                SqlDataAdapter customer_adapter1 = new SqlDataAdapter("select * from dbo.Customers", conn);
                SqlCommandBuilder cmd_customer1 = new SqlCommandBuilder(customer_adapter1);
                DataSet ds1 = new DataSet();
                customer_adapter1.Fill(ds1, "Customers");
                DataTable dt1 = ds1.Tables["Customers"];
                dt1.PrimaryKey = new DataColumn[] { dt1.Columns["id"] };
                DataRow row_del = dt1.Rows.Find(id_for_delete);
                if (row_del != null) 
                {
                    row_del.Delete();
                    Console.WriteLine("OK");
                }
                else Console.WriteLine("ERROR");

                customer_adapter1.Update(ds1, "Customers");
                dt.Clear();
                customer_adapter1.Fill(ds1, "Customers");

                foreach (DataRow item in dt1.Rows)
                {
                    Console.WriteLine($"{item[0],5} {item[1],20} {item[2],20} {Convert.ToDateTime(item[3]).ToShortDateString(),15}");
                }
            }
        }
    }
}