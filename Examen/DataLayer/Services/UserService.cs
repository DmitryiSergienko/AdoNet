using Model.Models;
using DataLayer.Procedures;
using Microsoft.Data.SqlClient;

namespace DataLayer.Services
{
    public class UserService
    {
        private readonly Models.TZ_21_07_2025Context _context_Models;
        private readonly Procedures.TZ_21_07_2025Context _context_Procedures;
        private readonly Views.TZ_21_07_2025Context _context_Views;

        public UserService(
            Models.TZ_21_07_2025Context contextModels,
            Procedures.TZ_21_07_2025Context contextProcedures,
            Views.TZ_21_07_2025Context contextViews)
        {
            _context_Models = contextModels;
            _context_Procedures = contextProcedures;
            _context_Views = contextViews;
        }

        public async Task<int> AddUserAsync(UsersModel user)
        {
            var userIdParam = new OutputParameter<int?>();

            try
            {
                await _context_Procedures.Procedures.add_user_return_idAsync(
                    login: user.Login,
                    password: user.Password,
                    name: user.Name,
                    surname: user.Surname,
                    patronymic: user.Patronymic,
                    mail: user.Mail,
                    phone_number: user.PhoneNumber,
                    userID: userIdParam
                );
            }
            catch (SqlException ex)
            {
                throw MapToUserFriendlyError(ex);
            }

            if (userIdParam.Value <= 0)
            {
                throw new Exception("Не удалось получить ID нового пользователя.");
            }
            return userIdParam.Value.Value;
        }

        private static Exception MapToUserFriendlyError(SqlException ex)
        {
            return ex.Message switch
            {
                string msg when msg.Contains("CHECK", StringComparison.OrdinalIgnoreCase)
                          && msg.Contains("password", StringComparison.OrdinalIgnoreCase)
                    => new ArgumentException("Пароль должен быть не менее 8 символов."),

                string msg when msg.Contains("UNIQUE", StringComparison.OrdinalIgnoreCase)
                          && msg.Contains("login", StringComparison.OrdinalIgnoreCase)
                    => new ArgumentException("Логин уже занят."),

                string msg when msg.Contains("UNIQUE", StringComparison.OrdinalIgnoreCase)
                          && msg.Contains("mail", StringComparison.OrdinalIgnoreCase)
                    => new ArgumentException("Email уже используется."),

                string msg when msg.Contains("NULL", StringComparison.OrdinalIgnoreCase)
                          && (msg.Contains("login") || msg.Contains("mail"))
                    => new ArgumentException("Логин и email обязательны."),

                _ => new Exception($"Ошибка при добавлении пользователя: {ex.Message}")
            };
        }
        public async Task<bool> OnLoginAsync(UsersModel user)
        {
            if (user == null)
            {
                return false;
            }
            try
            {
                var result = await _context_Procedures.Procedures.stp_search_user_for_authAsync(
                    login: user.Login,
                    password: user.Password
                );

                var userList = result.ToList();
                if (userList.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Ошибка базы данных: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                // Любая другая ошибка
                Console.WriteLine("Ошибка: " + ex.Message);
                return false;
            }
        }
    }
}