using DZ2_19_08_2025.DataLayer;
using DZ2_19_08_2025.Models;
using System;
using System.Collections.Generic;

namespace DZ2_19_08_2025
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var user = new UserModel(
                0,
                "Login_new",
                "Password_new",
                "Name_new",
                "Surname_new",
                "Patronymic_new",
                "Mail_new",
                "Phone_number_new",
                new DateTime(2024, 3, 15)
                );

            Console.WriteLine("Создан пользователь:");
            int id = DL.User.Insert(user);
            user.Id = id;
            Console.WriteLine(user + "\n");

            bool res = DL.User.Delete(user.Id);
            if (res) Console.WriteLine($"Пользователь c id '{id}' удален!");
            else Console.WriteLine("Ошибка при удалении пользователя!");

            Console.WriteLine("Создан пользователь:");
            id = DL.User.Insert(user);
            user.Id = id;
            Console.WriteLine(user);
            var new_user = new UserModel(
                user.Id,
                "new_log",
                "new_pass",
                "Иван",
                "Иванов",
                "Иванович",
                "ivan1@mail.ru",
                "+79001234568",
                new DateTime(2025, 7, 30)
                );
            res = DL.User.Update(new_user);
            if (res)
            {
                Console.WriteLine("Обновлен пользователь:");
                Console.WriteLine(new_user + "\n");
            }
            else Console.WriteLine("Ошибка при обновлении пользователя!");
            DL.User.Delete(new_user.Id);

            Console.WriteLine("Список пользователей:");
            List<UserModel> allUsers = DL.User.All();
            foreach (UserModel userTemp in allUsers)
            {
                Console.WriteLine(userTemp.ToString());
            }
        }
    }
}