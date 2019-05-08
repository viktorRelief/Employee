﻿using EmployeesProject.HashData;
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

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=viktor;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(
                new Department[]
                {
                    new Department { DepartmentId = 1, DepartmentName = "Kosmo"},
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
