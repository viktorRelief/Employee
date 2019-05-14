using Dapper;
using EmployeesProject.Interfaces;
using EmployeesProject.Models;
using Microsoft.AspNetCore.Authorization;
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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ILogger<DepartmentRepository> _logger;
        private readonly string _connectionString = null;
        public DepartmentRepository(string connectionString, ILogger<DepartmentRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        //To Get the list of Departments
        public async Task<List<Department>> GetDepartments()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var result = await db.QueryAsync<Department>("SELECT * FROM Department");

                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get departments failed: {ex}");
                throw;
            }
        }

        //To Add new department record
        public async Task AddDepartment(Department department)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var sqlQuery = "INSERT INTO Department (Name) VALUES(@Name)";
                    await db.ExecuteAsync(sqlQuery, department);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Add department failed: {ex}");
                throw;
            }
        }
    }
}
