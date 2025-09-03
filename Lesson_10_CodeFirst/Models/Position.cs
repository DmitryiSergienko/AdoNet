using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson_10_CodeFirst.Models
{
    [Table(name: "Positions")]
    public class Position
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Position_name { get; set; }

        // Коллекция сотрудников с этой должностью
        public virtual ICollection<Employee> Employees { get; set; }
    }
}