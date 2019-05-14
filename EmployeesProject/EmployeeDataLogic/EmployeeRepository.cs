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
        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public EmployeeRepository(ILogger<EmployeeRepository> logger)
        {
            _logger = logger;
        }

        //To get all list of employee
        public async Task<IEnumerable<Employee>> GetAllEmployees()
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
            catch(Exception ex)
            {
                _logger.LogInformation(ex, "Get employees failed");
                throw;
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
                _logger.LogInformation(ex, "Add employees failed");
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
                _logger.LogInformation(ex, "Update employees failed");
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
                _logger.LogInformation(ex, "Get employees data failed");
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
                    return await db.QueryFirstOrDefaultAsync<Employee>("SELECT * FROM c WHERE Id = @id", new { id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Confirm delete employees failed");
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
                _logger.LogInformation(ex, "Delete employee failed");
                throw;
            }
        }
    }
}
