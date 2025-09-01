using System;

namespace Lesson_8_EFW_DBFirst.Models
{
    public class CustomerModel
    {
        private DateTime birthDate;

        public CustomerModel()
        {
        }

        public CustomerModel(int iD, string firstName, string lastName, DateTime birthDate)
        {
            id = iD;
            FirstName = firstName;
            LastName = lastName;
            this.birthDate = birthDate;
        }

        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public byte[] Picture { get; set; }
        public override string ToString()
        {
            return $"{id,5} {LastName,20} {FirstName,20} {Convert.ToDateTime(DateOfBirth).ToShortDateString(),20}";
        }
    }
}