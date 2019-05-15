using Dapper;
using EmployeesProject.Interfaces;
using EmployeesProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesProject.EmployeeDataLogic
{
    [Authorize]
    public class EmployeeRepository : Controller, IEmployeeRepository
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private string _connectionString = null;

        public EmployeeRepository(ILogger<EmployeeRepository> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {         
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    var result = await db.QueryAsync<Employee, Department, Employee>("SELECT * FROM Employee JOIN Department ON Employee.DepartmentId = Department.Id", (employee, department) =>
                    {
                        employee.Department = department;

                        return employee;
                    });

                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get employees failed: {ex}");
                throw;
            }
        }
 
        public async Task Add(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (IDbConnection db = new SqlConnection(_connectionString))
                    {
                        var sqlQuery = "INSERT INTO Employee (EmployeeLogin, FirstName, LastName, PhoneNumber, Email, HomeAddress, DepartmentId) VALUES(@EmployeeLogin, @FirstName, @LastName, @PhoneNumber, @Email, @HomeAddress, @DepartmentId)";
                        await db.ExecuteAsync(sqlQuery, employee);
                    }
                }
                else
                {
                    _logger.LogInformation("Model validation failed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Add employees failed: {ex}");
                throw;
            }
        }

        public async Task Update(Employee employee)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var sqlQuery = "UPDATE Employee SET EmployeeLogin = @EmployeeLogin, FirstName = @FirstName, LastName = @LastName, PhoneNumber = @PhoneNumber, Email = @Email, HomeAddress = @HomeAddress, DepartmentId = @DepartmentId WHERE Id = @Id";
                    await db.ExecuteAsync(sqlQuery, employee);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update employees failed: {ex}");
                throw;
            }
        }

        public async Task<Employee> GetData(int id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    return await db.QueryFirstOrDefaultAsync<Employee>("SELECT * FROM Employee WHERE Id = @id", new { id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get employees data failed: {ex}");
                throw;
            }
        }

        public async Task<Employee> ConfirmDelete(int id)
        {
            try
            {          
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    return await db.QueryFirstOrDefaultAsync<Employee>("SELECT * FROM Employee WHERE Id = @id", new { id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Confirm delete employees failed: {ex}");
                throw;
            }
        }
 
        public async Task Delete(int id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var sqlQuery = "DELETE FROM Employee WHERE Id = @id";
                    await db.ExecuteAsync(sqlQuery, new { id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete employee failed: {ex}");
                throw;
            }
        }
    }
}
