using System;

namespace Model.Models
{
    public class UsersModel
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public string mail { get; set; }
        public string phone_number { get; set; }
        public Nullable<System.DateTime> registration_date { get; set; }
        public override string ToString()
        {
            return $"{id,5} {login,15} {name,15} {surname,15} {mail,20} {Convert.ToDateTime(registration_date),20}";
        }
    }
}