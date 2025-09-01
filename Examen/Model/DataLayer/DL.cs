using Model.Models;
using System.Configuration;

namespace Model.DataLayer
{
    public class DL
    {
        public static string ConnectionString { get; private set; } = ConfigurationManager.ConnectionStrings["TZ_21_07_2025"].ConnectionString;
        public static class User
        {
            public static int Insert(UsersModel tmp)
            {
                using (var db = new TZ_21_07_2025Entities())
                {
                    var customerID = new System.Data.Entity.Core.Objects.ObjectParameter("CustomerId", typeof(int));

                    var res = db.add_user_return_id(
                        tmp.login,
                        tmp.password,
                        tmp.name,
                        tmp.surname,
                        tmp.patronymic,
                        tmp.mail,
                        tmp.phone_number,
                        customerID
                        );

                    return res;
                }
            }
        }
    }
}