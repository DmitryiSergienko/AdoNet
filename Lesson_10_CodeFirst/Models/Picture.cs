using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson_10_CodeFirst.Models
{
    [Table("Pictures")]
    public class Picture
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name_pict { get; set; }
        [Required]
        public int CustomerID { get; set; }
        public byte[] Pictures { get; set; }

        public virtual Customer Customer { get; set; }
    }
}