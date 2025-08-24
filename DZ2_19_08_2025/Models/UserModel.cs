using System;

namespace DZ2_19_08_2025.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }

        public UserModel(
            int id, 
            string login, 
            string password, 
            string name, 
            string surname, 
            string patronymic, 
            string mail, 
            string phoneNumber, 
            DateTime registrationDate)
        {
            Id = id;
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Mail = mail;
            PhoneNumber = phoneNumber;
            RegistrationDate = registrationDate;
        }
        public override string ToString() 
        {
            return $"" +
                $"{Id,-4} " +
                $"{Login,-15} " +
                $"{Name,-15} " +
                $"{Surname,-15} " +
                $"{Mail,-15} " +
                $"{RegistrationDate.ToShortDateString(),-15}";
        }
    }
}