using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using EmployeesProject.Interfaces;
using EmployeesProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace EmployeesProject.EmployeeDataLogic
{
    [Authorize]
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ILogger<EmployeeRepository> _logger;
        private readonly string _connectionString = null;
        public EmployeeRepository(string connectionString, ILogger<EmployeeRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        //To get all list of employee
        public async Task<IEnumerable<Employee>> GetAllEmployees()
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

        //To Add new employee record   
        public async Task AddEmployee(Employee employee)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var sqlQuery = "INSERT INTO Employee (EmployeeLogin, FirstName, LastName, PhoneNumber, Email, HomeAddress, DepartmentId) VALUES(@EmployeeLogin, @FirstName, @LastName, @PhoneNumber, @Email, @HomeAddress, @DepartmentId)";
                    await db.ExecuteAsync(sqlQuery, employee);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Add employees failed: {ex}");
                throw;
            }
        }

        //To Update the records of a particluar employee  
        public async Task UpdateEmployee(Employee employee)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var sqlQuery = "UPDATE Employee SET EmployeeLogin = @EmployeeLogin, FirstName = @FirstName, PhoneNumber = @PhoneNumber, Email = @Email, HomeAddress = @HomeAddress, DepartmentId = @DepartmentId WHERE Id = @Id";
                    await db.ExecuteAsync(sqlQuery, employee);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update employees failed: {ex}");
                throw;
            }
        }

        //Get the details of a particular employee  
        public async Task<Employee> GetEmployeeData(int id)
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

        //To Delete the record of a particular employee  
        public async Task<Employee> ConfirmDeleteEmployee(int id)
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
 
        public async Task DeleteEmployee(int id)
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
