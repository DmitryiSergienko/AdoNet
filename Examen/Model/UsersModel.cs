namespace Model
{
    public class UsersModel : HumansModel
    {
        public UsersModel(int id, string login, string password, string name, string surname, string patronymic, string mail, string phone_number, DateTime? registration_date, int images_id)
            : base(id, login, password, name, surname, patronymic, mail, phone_number, registration_date, images_id) { }

        public UsersModel(string login, string password, string name, string surname, string patronymic, string mail, string phone_number, int images_id)
            : base(login, password, name, surname, patronymic, mail, phone_number, images_id) { }
        public UsersModel(string login, string password, string name, string surname, string patronymic, string mail, string phone_number)
            : base(login, password, name, surname, patronymic, mail, phone_number) { }
        public UsersModel(string login, string password)
            : base(login, password) { }
    }
}