using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using EmployeeManagement.Shared.Models;

namespace EmployeeManagement.API.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }
        public DbSet<EmployeeManagement.Shared.Models.Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Salary)
            .HasPrecision(18, 2);
    }
}
}
