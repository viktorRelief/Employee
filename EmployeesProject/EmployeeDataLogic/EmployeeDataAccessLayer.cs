using EmployeesProject.Controllers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesProject.Models
{
    [Authorize]
    public class EmployeeDataAccessLayer : IEmployeeDataAccessLayer
    {
        private readonly ModelContext _db;
        private readonly ILogger<EmployeeDataAccessLayer> _logger;
        public EmployeeDataAccessLayer(ModelContext db, ILogger<EmployeeDataAccessLayer> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            try
            {
                return await _db.Employee.ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Get employees failed " + ex.Message);
                throw new Exception("Get employee internal server error");
            }
        }

        //To Add new employee record   
        public async Task<int> AddEmployee(Employee employee)
        {
            try
            {
                await _db.Employee.AddAsync(employee);
                await _db.SaveChangesAsync();
                return 1;
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Add employees failed " + ex.Message);
                throw new Exception("Add employee internal server error");
            }
        }

        //To Update the records of a particluar employee  
        public async Task<int> UpdateEmployee(Employee employee)
        {
            try
            {
                _db.Entry(employee).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return 1;
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Update employees failed" + ex.Message);
                throw new Exception("Update employee internal server error");
            }
        }

        //Get the details of a particular employee  
        public async Task<Employee> GetEmployeeData(int id)
        {
            try
            {
                Employee employee = await _db.Employee.FindAsync(id);
                return employee;
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Get employees data failed" + ex.Message);
                throw new Exception("Get employee data internal server error");
            }
        }

        //To Delete the record of a particular employee  
        public async Task<Employee> ConfirmDelete(int id)
        {
            try
            {
                Employee emp = await _db.Employee.FirstOrDefaultAsync(p => p.EmployeeId == id);
                return emp;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Confirm deleate employees failed" + ex.Message);
                throw new Exception("Confirm deleate employee internal server error");
            }
        }

        public async Task<int> DeleteEmployee(int id)
        {
            try
            {
                Employee emp = await _db.Employee.FindAsync(id);
                _db.Employee.Remove(emp);
                await _db.SaveChangesAsync();
                return 1;
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Deleate employees failed" + ex.Message);
                throw new Exception("Deleate employee internal server error");
            }
        }

        //To Get the list of Departments  
        public async Task<List<Department>> GetDepartments()
        {
            try
            {
                List<Department> lstDepartments = new List<Department>();
                lstDepartments = await (from DepartmentList in _db.Departments select DepartmentList).ToListAsync();

                return lstDepartments;
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Get departments failed" + ex.Message);
                throw new Exception("Get departments internal server error");
            }
        }
    }
}
