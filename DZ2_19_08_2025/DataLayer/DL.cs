using DZ2_19_08_2025.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace DZ2_19_08_2025.DataLayer
{
    public class DL
    {
        public static string ConnectionString { get; private set; } = ConfigurationManager.ConnectionStrings["TZ_21_07_2025"].ConnectionString;
        static SqlConnection conn;

        public static class User
        {
            public static List<UserModel> All()
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string custAll = "dbo.stp_users_all";
                    SqlCommand cmd = new SqlCommand(custAll, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dr = cmd.ExecuteReader();
                    List<UserModel> allUsers = new List<UserModel>();
                    while (dr.Read())
                    {
                        int id = (int)dr[0];
                        string login = dr[1].ToString();
                        string password = dr[2].ToString();
                        string name = dr[3].ToString();
                        string surname = dr[4].ToString();
                        string patronymic = dr[5].ToString();
                        string mail = dr[6].ToString();
                        string phoneNumber = dr[7].ToString();
                        DateTime registrationDate = DateTime.Parse(dr[8].ToString());
                        UserModel user = new UserModel(
                            id,
                            login,
                            password,
                            name,
                            surname,
                            patronymic,
                            mail,
                            phoneNumber,
                            registrationDate
                            );
                        allUsers.Add(user);
                    }
                    dr.Close();
                    return allUsers;
                }
            }
            public static int Insert(UserModel tmp)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string add_user = "dbo.add_user";
                    SqlCommand cmd = new SqlCommand(add_user, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlCommandBuilder.DeriveParameters(cmd);
                    cmd.Parameters[8].Value = DBNull.Value;
                    cmd.Parameters[1].Value = tmp.Login;
                    cmd.Parameters[2].Value = tmp.Password;
                    cmd.Parameters[3].Value = tmp.Name;
                    cmd.Parameters[4].Value = tmp.Surname;
                    cmd.Parameters[5].Value = tmp.Patronymic;
                    cmd.Parameters[6].Value = tmp.Mail;
                    cmd.Parameters[7].Value = tmp.PhoneNumber;
                    cmd.ExecuteNonQuery();
                    int new_id = (int)cmd.Parameters[8].Value;
                    return new_id;
                }
            }
            public static bool Delete(int userId)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string command = "[dbo].[stp_user_delete]";
                    SqlCommand cmd = new SqlCommand(command, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", userId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            public static bool Update(UserModel user)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("stp_user_update", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", user.Id);
                    cmd.Parameters.AddWithValue("@login", user.Login);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@surname", user.Surname);
                    cmd.Parameters.AddWithValue("@patronymic", user.Patronymic);
                    cmd.Parameters.AddWithValue("@mail", user.Mail);
                    cmd.Parameters.AddWithValue("@phone_number", user.PhoneNumber);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}