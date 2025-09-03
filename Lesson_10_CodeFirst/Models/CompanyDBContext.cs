using System.Data.Entity;

namespace Lesson_10_CodeFirst.Models
{
    public class CompanyDBContext : DbContext
    {
        public CompanyDBContext() : base("CompanyDB_CodeFirst") { }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
    }
}