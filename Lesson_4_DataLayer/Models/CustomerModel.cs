using System;

namespace Lesson_4_DataLayer.Models
{
    public class CustomerModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public CustomerModel(int iD, string firstName, string lastName, DateTime birthDate)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }
        public override string ToString()
        {
            return $"{ID,-4} {FirstName,-15} {LastName,-15} {BirthDate.ToShortDateString(),-15}";
        }
    }
}