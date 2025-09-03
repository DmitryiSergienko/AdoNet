using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson_10_CodeFirst.Models
{
    [Table(name: "Employees")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public DateTime? DateOfBirth { get; set; }

        // Внешний ключ на Position
        [Required]
        public int PositionId { get; set; }

        // Навигация
        public virtual Position Position { get; set; }
    }
}