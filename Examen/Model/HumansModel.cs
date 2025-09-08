using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace Model
{
    abstract public class HumansModel
    {
        public HumansModel(int id, string login, string password, string name, string surname, string patronymic, string mail, string phone_number, DateTime? registration_date, int images_id)
        {
            Id = id;
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Mail = mail;
            PhoneNumber = phone_number;
            Registration_date = registration_date;
            Images_id = images_id;
        }
        public HumansModel(string login, string password, string name, string surname, string patronymic, string mail, string phone_number, int images_id)
        {
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Mail = mail;
            PhoneNumber = phone_number;
            Images_id = images_id;
        }
        public HumansModel(string login, string password, string name, string surname, string patronymic, string mail, string phone_number)
        {
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Mail = mail;
            PhoneNumber = phone_number;
        }
        public HumansModel(string login, string password)
        {
            Login = login;
            Password = password;
        }
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<System.DateTime> Registration_date { get; set; }
        public int? Images_id { get; set; }
        public virtual ImagesModel Image { get; set; }
        public override string ToString()
        {
            return $"{Id,5} {Login,15} {Name,15} {Surname,15} {Mail,20} {Convert.ToDateTime(Registration_date),20}";
        }
    }
}