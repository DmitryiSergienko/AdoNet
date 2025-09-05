using DataLayer.Procedures;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System.Data;

namespace DataLayer.Services
{
    public class UserService
    {
        public UsersModel? CurrentUser => _currentUser;
        private UsersModel? _currentUser;
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
            var userIdParam = new Procedures.OutputParameter<int?>();

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

                GetUserInfoAsync(user);

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
        private async Task GetUserInfoAsync(UsersModel user)
        {
            var result = await _context_Procedures.Procedures.stp_search_user_for_infoAsync(user.Login);
            var userList = result.ToList();

            if (userList.Any())
            {
                var dbUser = userList.First();

                _currentUser = new UsersModel(
                    login: dbUser.login,
                    password: dbUser.password,
                    name: dbUser.name,
                    surname: dbUser.surname,
                    patronymic: dbUser.patronymic,
                    mail: dbUser.mail,
                    phone_number: dbUser.phone_number
                );
            }
        }
        public void LogOut()
        {
            _currentUser = null;
        }
        public async Task<DataTable> ExecuteStoredProcedureAsync(string procedureName, params (string Name, object Value)[] parameters)
        {
            var dataTable = new DataTable();

            using var connection = _context_Procedures.Database.GetDbConnection();
            using var command = connection.CreateCommand();
            command.CommandText = procedureName;
            command.CommandType = CommandType.StoredProcedure;

            foreach (var (Name, Value) in parameters)
            {
                var param = command.CreateParameter();
                param.ParameterName = Name.StartsWith("@") ? Name : "@" + Name;
                param.Value = Value ?? DBNull.Value;
                command.Parameters.Add(param);
            }

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            dataTable.Load(reader);

            return dataTable;
        }
        private DataTable ConvertToDataTable<T>(IEnumerable<T> data)
        {
            var dataTable = new DataTable();
            var type = typeof(T);
            var props = type.GetProperties();

            foreach (var prop in props)
            {
                var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                dataTable.Columns.Add(prop.Name, propType);
            }

            foreach (var item in data)
            {
                var row = dataTable.NewRow();
                foreach (var prop in props)
                {
                    var value = prop.GetValue(item);
                    row[prop.Name] = value ?? DBNull.Value;
                }
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
        public async Task<DataTable> GetViewDataAsync<T>(IQueryable<T> query)
        {
            var dataTable = new DataTable();

            var data = await query.ToListAsync();

            if (!data.Any()) return dataTable;

            var type = typeof(T);
            var props = type.GetProperties();

            foreach (var prop in props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (var item in data)
            {
                var row = dataTable.NewRow();
                foreach (var prop in props)
                {
                    var value = prop.GetValue(item);
                    row[prop.Name] = value ?? DBNull.Value;
                }
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
        public async Task<DataTable> GetTop3ProductsAsync()
        {
            return await GetViewDataAsync(_context_Views.show_top_3_products);
        }
        public async Task<DataTable> GetShowProductsInPortionsAsync(int skipRows, int showRows)
        {
            // Вызываем процедуру — она возвращает List<T>
            var resultList = await _context_Procedures.Procedures.show_products_in_portionsAsync(
                skip_rows: skipRows,
                show_rows: showRows
            );

            // Преобразуем List<T> в DataTable — без асинхронных операций
            return ConvertToDataTable(resultList);
        }
        public async Task<DataTable> GetAllProducts()
        {
            // Вызываем процедуру — она возвращает List<T>
            var resultList = await _context_Procedures.Procedures.show_all_productsAsync();

            // Преобразуем List<T> в DataTable — без асинхронных операций
            return ConvertToDataTable(resultList);
        }
        public async Task<List<show_all_productsResult>> GetListAllProductsAsync()
        {
            return await _context_Procedures.Procedures.show_all_productsAsync();
        }
        public async Task<DataTable> GetUserOrderHistoryAsync() //Подумать
        {
            return await GetViewDataAsync(_context_Views.show_number_of_users_orders);
        }
        public async Task<DataTable> GetUsersWithoutPasswordAsync() //Подумать
        {
            return await GetViewDataAsync(_context_Views.show_users_without_passwords);
        }
    }
}