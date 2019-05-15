using Dapper;
using EmployeesProject.Interfaces;
using EmployeesProject.Models;
using Microsoft.AspNetCore.Authorization;
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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private readonly string _connectionString = null;
        public DepartmentRepository(ILogger<DepartmentRepository> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Department>> GetAll()
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

        public async Task Add(Department department)
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
