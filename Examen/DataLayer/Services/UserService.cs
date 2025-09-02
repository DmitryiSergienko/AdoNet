using Microsoft.EntityFrameworkCore;
using Model.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DataLayer.Services
{
    public class UserService
    {
        private readonly TZ_21_07_2025Context_Main _context;

        public UserService(TZ_21_07_2025Context_Main context)
        {
            _context = context;
        }

        public async Task<int> AddUserAsync(UsersModel user)
        {
            var customerIdParam = new SqlParameter("@CustomerId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC add_user_return_id @login, @password, @name, @surname, @patronymic, @mail, @phone_number, @CustomerId OUT",
                new SqlParameter("@login", user.Login ?? (object)DBNull.Value),
                new SqlParameter("@password", user.Password ?? (object)DBNull.Value),
                new SqlParameter("@name", user.Name ?? (object)DBNull.Value),
                new SqlParameter("@surname", user.Surname ?? (object)DBNull.Value),
                new SqlParameter("@patronymic", user.Patronymic ?? (object)DBNull.Value),
                new SqlParameter("@mail", user.Mail ?? (object)DBNull.Value),
                new SqlParameter("@phone_number", user.PhoneNumber ?? (object)DBNull.Value),
                customerIdParam
            );

            return (int)customerIdParam.Value;
        }
    }
}