using DataLayer.Models;
using DataLayer.Procedures;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Data;
using System.Data.Entity;

namespace DataLayer.Services
{
    public class Service
    {
        public HumansModel? CurrentHuman => _currentHuman;
        public HumansModel? DeleteHuman
        {
            get => _deleteHuman;
            set => _deleteHuman = value;
        }
        private HumansModel? _currentHuman;
        private HumansModel? _deleteHuman;

        private readonly Models.DataBaseContext _context_Models;
        private readonly Procedures.DataBaseContext _context_Procedures;
        private readonly Views.DataBaseContext _context_Views;

        public Service(
            Models.DataBaseContext contextModels,
            Procedures.DataBaseContext contextProcedures,
            Views.DataBaseContext contextViews)
        {
            _context_Models = contextModels;
            _context_Procedures = contextProcedures;
            _context_Views = contextViews;
        }

        public async Task<int> AddHumanAsync(HumansModel human)
        {
            var humanIdParam = new Procedures.OutputParameter<int?>();

            try
            {
                if (human is UsersModel)
                    await _context_Procedures.Procedures.add_user_return_idAsync(
                        login: human.Login,
                        password: human.Password,
                        name: human.Name,
                        surname: human.Surname,
                        patronymic: human.Patronymic,
                        mail: human.Mail,
                        phone_number: human.PhoneNumber,
                        userID: humanIdParam
                );
                else
                {
                    await _context_Procedures.Procedures.add_admin_return_idAsync(
                        login: human.Login,
                        password: human.Password,
                        name: human.Name,
                        surname: human.Surname,
                        patronymic: human.Patronymic,
                        mail: human.Mail,
                        phone_number: human.PhoneNumber,
                        adminID: humanIdParam
                    );
                }
            }
            catch (SqlException ex)
            {
                throw MapToUserFriendlyError(ex);
            }

            if (humanIdParam.Value <= 0)
            {
                throw new Exception("Не удалось получить ID.");
            }
            return humanIdParam.Value.Value;
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
        public async Task<bool> OnLoginAsync(HumansModel human)
        {
            if (human == null)
            {
                return false;
            }
            try
            {
                IEnumerable<dynamic> result = null;

                if (human is AdminsModel)
                {
                    result = await _context_Procedures.Procedures.stp_search_admin_for_authAsync(human.Login, human.Password);
                }                       
                else if (human is UsersModel)
                {
                    result = await _context_Procedures.Procedures.stp_search_user_for_authAsync(human.Login, human.Password);
                }                    
                await GetHumanInfoAsync(human);
                return result.Any();
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
        public async Task<HumansModel> GetHumanInfoById(int Id, string humanType)
        {
            _deleteHuman = null;

            if (humanType == "user")
            {
                var result = await _context_Procedures.Procedures.stp_search_user_by_idAsync(Id);
                var dbUser = result.FirstOrDefault();

                if (dbUser == null)
                {
                    return null;
                }

                _deleteHuman = new UsersModel(
                    login: dbUser.login,
                    password: dbUser.password,
                    name: dbUser.name,
                    surname: dbUser.surname,
                    patronymic: dbUser.patronymic,
                    mail: dbUser.mail,
                    phone_number: dbUser.phone_number
                );
                return _deleteHuman;
            }
            else if (humanType == "admin")
            {
                var result = await _context_Procedures.Procedures.stp_search_admin_by_idAsync(Id);
                var dbAdmin = result.FirstOrDefault();

                if (dbAdmin == null)
                {
                    return null;
                }

                _deleteHuman = new AdminsModel(
                    login: dbAdmin.login,
                    password: dbAdmin.password,
                    name: dbAdmin.name,
                    surname: dbAdmin.surname,
                    patronymic: dbAdmin.patronymic,
                    mail: dbAdmin.mail,
                    phone_number: dbAdmin.phone_number
                );
                return _deleteHuman;
            }
            return null;
        }
        private async Task GetHumanInfoAsync(HumansModel human)
        {
            if (human is AdminsModel admin)
            {
                var result = await _context_Procedures.Procedures.stp_search_admin_for_infoAsync(human.Login);
                var dbAdmin = result.FirstOrDefault();
                if (dbAdmin != null)
                {
                    _currentHuman = new AdminsModel(
                        login: dbAdmin.login,
                        password: dbAdmin.password,
                        name: dbAdmin.name,
                        surname: dbAdmin.surname,
                        patronymic: dbAdmin.patronymic,
                        mail: dbAdmin.mail,
                        phone_number: dbAdmin.phone_number
                    );
                }
            }
            else if (human is UsersModel user)
            {
                var result = await _context_Procedures.Procedures.stp_search_user_for_infoAsync(human.Login);
                var dbUser = result.FirstOrDefault();
                if (dbUser != null)
                {
                    _currentHuman = new UsersModel(
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
        }
        public void LogOut()
        {
            _currentHuman = null;
        }
        public async Task<DataTable> ExecuteStoredProcedureAsync(string procedureName, params (string Name, object Value)[] parameters)
        {
            var dataTable = new DataTable();

            using var connection = _context_Procedures.Database.GetDbConnection();

            // Проверка: если ConnectionString не задан — ошибка
            if (string.IsNullOrEmpty(connection.ConnectionString))
            {
                throw new InvalidOperationException("ConnectionString has not been initialized.");
            }

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
            return await ExecuteSqlQueryAsync("SELECT * FROM show_top_3_products");
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
            return await ExecuteSqlQueryAsync("SELECT * FROM show_number_of_users_orders");
        }
        public async Task<DataTable> GetUsersWithoutPasswordAsync() //Подумать
        {
            return await GetViewDataAsync(_context_Views.show_users_without_passwords);
        }
        public async Task AssignImageToAdmin(int adminId, int imageId)
        {
            try
            {
                // ✅ Синхронные вызовы — чтобы избежать ошибки с IQueryable
                var admin = _context_Models.adminsSets.FirstOrDefault(a => a.id == adminId);
                if (admin == null)
                {
                    Console.WriteLine($"❌ Админ с ID {adminId} не найден!");
                    throw new ArgumentException($"Админ с ID {adminId} не найден.");
                }

                var image = _context_Models.imagesSets.FirstOrDefault(i => i.id == imageId);
                if (image == null)
                {
                    Console.WriteLine($"❌ Картинка с ID {imageId} не найдена!");
                    throw new ArgumentException($"Картинка с ID {imageId} не найдена.");
                }

                admin.images_id = imageId;
                await _context_Models.SaveChangesAsync();
                Console.WriteLine($"✅ Успешно привязали картинку ID {imageId} к админу ID {adminId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка при привязке картинки к админу ID {adminId}: {ex.Message}");
                throw;
            }
        }
        public async Task AssignImageToUser(int userId, int imageId)
        {
            try
            {
                // ✅ Синхронные вызовы — чтобы избежать ошибки с IQueryable
                var user = _context_Models.usersSets.FirstOrDefault(u => u.id == userId);
                if (user == null)
                {
                    Console.WriteLine($"❌ Пользователь с ID {userId} не найден!");
                    throw new ArgumentException($"Пользователь с ID {userId} не найден.");
                }

                var image = _context_Models.imagesSets.FirstOrDefault(i => i.id == imageId);
                if (image == null)
                {
                    Console.WriteLine($"❌ Картинка с ID {imageId} не найдена!");
                    throw new ArgumentException($"Картинка с ID {imageId} не найдена.");
                }

                user.images_id = imageId;
                await _context_Models.SaveChangesAsync();
                Console.WriteLine($"✅ Успешно привязали картинку ID {imageId} к пользователю ID {userId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка при привязке картинки к пользователю ID {userId}: {ex.Message}");
                throw;
            }
        }
        public async Task AssignImageToProduct(int productId, int imageId)
        {
            try
            {
                // ✅ Синхронные вызовы — чтобы избежать ошибки с IQueryable
                var product = _context_Models.productsSets.FirstOrDefault(p => p.id == productId);
                if (product == null)
                {
                    Console.WriteLine($"❌ Продукт с ID {productId} не найден!");
                    throw new ArgumentException($"Продукт с ID {productId} не найден.");
                }

                var image = _context_Models.imagesSets.FirstOrDefault(i => i.id == imageId);
                if (image == null)
                {
                    Console.WriteLine($"❌ Картинка с ID {imageId} не найдена!");
                    throw new ArgumentException($"Картинка с ID {imageId} не найдена.");
                }

                product.images_id = imageId;
                await _context_Models.SaveChangesAsync();
                Console.WriteLine($"✅ Успешно привязали картинку ID {imageId} к продукту ID {productId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка при привязке картинки к продукту ID {productId}: {ex.Message}");
                throw;
            }
        }
        public async Task<int> AddImageAsync(string fileName, string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Файл не найден: {filePath}");

            byte[] imageData = await File.ReadAllBytesAsync(filePath);

            var image = new imagesSet
            {
                name = fileName,
                image = imageData
            };

            _context_Models.imagesSets.Add(image);
            await _context_Models.SaveChangesAsync();

            return image.id;
        }
        public async Task<Dictionary<string, int>> BulkAddImagesFromFolderAsync(string folderPath)
        {
            var result = new Dictionary<string, int>();

            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"Папка не найдена: {folderPath}");

            var files = Directory.GetFiles(folderPath, "*.*")
                .Where(f => f.EndsWith(".jpg") || f.EndsWith(".png") || f.EndsWith(".jpeg"));

            foreach (var filePath in files)
            {
                string fileName = Path.GetFileName(filePath);
                int imageId = await AddImageAsync(fileName, filePath);
                result[fileName] = imageId;
            }

            return result;
        }
        public async Task AddPicturesToAllTablesAsync()
        {
            try
            {
                Console.WriteLine("🚀 [AddPicturesToAllTablesAsync] Начинаем выполнение...");

                // 1. Определяем путь к папке Resources
                string resourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");

                // ✅ ЛОГИРУЕМ ПУТЬ И ПРОВЕРЯЕМ СУЩЕСТВОВАНИЕ ПАПКИ
                Console.WriteLine($"🔍 Путь к папке с картинками: {resourcesPath}");
                if (!Directory.Exists(resourcesPath))
                {
                    Console.WriteLine($"❌ Папка не найдена: {resourcesPath}");
                    throw new DirectoryNotFoundException($"Папка с картинками не найдена: {resourcesPath}");
                }

                // Ищем файлы
                var files = Directory.GetFiles(resourcesPath, "*.*")
                    .Where(f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                                f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                Console.WriteLine($"📂 Найдено файлов: {files.Count}");
                foreach (var file in files)
                {
                    Console.WriteLine($"📄 {Path.GetFileName(file)}");
                }

                if (files.Count == 0)
                {
                    Console.WriteLine("⚠️ В папке нет подходящих файлов (.jpg, .png, .jpeg)");
                    return;
                }

                // 2. Загружаем картинки в БД
                Console.WriteLine("⏳ Загружаем картинки в imagesSets...");
                var imageMap = await BulkAddImagesFromFolderAsync(resourcesPath);
                Console.WriteLine($"✅ Загружено в imagesSet: {imageMap.Count} картинок");

                if (imageMap.Count == 0)
                {
                    Console.WriteLine("⚠️ Ни одна картинка не была загружена в БД");
                    return;
                }

                // 3. Привязываем к админам
                Console.WriteLine("👥 Загружаем админов...");
                var admins = _context_Models.adminsSets.ToList(); // ← Синхронно, чтобы избежать ошибки
                Console.WriteLine($"✅ Найдено админов: {admins.Count}");

                foreach (var admin in admins)
                {
                    var matchedFile = imageMap.Keys
                        .FirstOrDefault(k => Path.GetFileNameWithoutExtension(k)
                            .Equals(admin.login, StringComparison.OrdinalIgnoreCase));

                    if (!string.IsNullOrEmpty(matchedFile))
                    {
                        int imageId = imageMap[matchedFile];
                        Console.WriteLine($"✅ Присваиваем картинку '{matchedFile}' админу '{admin.login}' (ID: {admin.id})");
                        await AssignImageToAdmin(admin.id, imageId);
                        Console.WriteLine($"✅ Сохранено для админа ID {admin.id}");
                    }
                    else
                    {
                        Console.WriteLine($"❌ Не найдена картинка для админа: {admin.login}");
                    }
                }

                // 4. Привязываем к пользователям
                Console.WriteLine("👥 Загружаем пользователей...");
                var users = _context_Models.usersSets.ToList(); // ← Синхронно
                Console.WriteLine($"✅ Найдено пользователей: {users.Count}");

                foreach (var user in users)
                {
                    var matchedFile = imageMap.Keys
                        .FirstOrDefault(k => Path.GetFileNameWithoutExtension(k)
                            .Equals(user.login, StringComparison.OrdinalIgnoreCase));

                    if (!string.IsNullOrEmpty(matchedFile))
                    {
                        int imageId = imageMap[matchedFile];
                        Console.WriteLine($"✅ Присваиваем картинку '{matchedFile}' пользователю '{user.login}' (ID: {user.id})");
                        await AssignImageToUser(user.id, imageId);
                        Console.WriteLine($"✅ Сохранено для пользователя ID {user.id}");
                    }
                    else
                    {
                        Console.WriteLine($"❌ Не найдена картинка для пользователя: {user.login}");
                    }
                }

                // 5. Привязываем к продуктам
                Console.WriteLine("📦 Загружаем продукты...");
                var products = _context_Models.productsSets.ToList(); // ← Синхронно
                Console.WriteLine($"✅ Найдено продуктов: {products.Count}");

                foreach (var product in products)
                {
                    var matchedFile = imageMap.Keys
                        .FirstOrDefault(k => Path.GetFileNameWithoutExtension(k)
                            .Equals(product.name, StringComparison.OrdinalIgnoreCase));

                    if (!string.IsNullOrEmpty(matchedFile))
                    {
                        int imageId = imageMap[matchedFile];
                        Console.WriteLine($"✅ Присваиваем картинку '{matchedFile}' продукту '{product.name}' (ID: {product.id})");
                        await AssignImageToProduct(product.id, imageId);
                        Console.WriteLine($"✅ Сохранено для продукта ID {product.id}");
                    }
                    else
                    {
                        Console.WriteLine($"❌ Не найдена картинка для продукта: {product.name}");
                    }
                }

                Console.WriteLine("🎉 [AddPicturesToAllTablesAsync] Привязка картинок завершена!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ОШИБКА в AddPicturesToAllTablesAsync: {ex.Message}");
                Console.WriteLine($"📝 Стек: {ex.StackTrace}");
                throw;
            }
        }
        public async Task<DataTable> ExecuteSqlQueryAsync(string sqlQuery)
        {
            var dataTable = new DataTable();
            using var connection = _context_Procedures.Database.GetDbConnection();
            using var command = connection.CreateCommand();
            command.CommandText = sqlQuery;
            command.CommandType = CommandType.Text;
            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            dataTable.Load(reader);
            return dataTable;
        }
    }
}