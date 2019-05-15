using EmployeesProject.HashData;
using Microsoft.EntityFrameworkCore;

namespace EmployeesProject.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
            Database.EnsureCreated();
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=viktor;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee[]
                {
                    new Employee { Id = 1, EmployeeLogin = "login", FirstName = "name", LastName = "surname", PhoneNumber = "1234", Email = "qqq@gmmail.com", HomeAddress = "street", DepartmentId = 1},
                });

            modelBuilder.Entity<Department>().HasData(
                new Department[]
                {
                    new Department { Id = 1, Name = "Kosmo"},
                });

            modelBuilder.Entity<User>().HasData(
                new User[]
                {
                    new User { Id = 1, Email = "admin", Password = SecurePasswordHasherHelper.Hash("123123")},
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
