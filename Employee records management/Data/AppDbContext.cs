using EmployeeRecordsManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRecordsManagement.Data
{
    public class AppDbContext : DbContext
    {
        //Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //DbSet properties
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
