using EmployeesProject.Controllers.Interfaces;
using EmployeesProject.Interfaces;
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
    public class EmployeeDataAccessLayer : IEmployeeDataAccessLayer, IDepartmentDataAccessLayer
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
                throw;
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
                throw;
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
                throw;
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
                throw;
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
                _logger.LogInformation(ex, "Confirm delete employees failed");
                throw;
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
                _logger.LogInformation(ex, "Delete employee failed");
                throw;
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
                throw;
            }
        }

        //To Add new department record   
        public async Task<int> AddDepartment(Department department)
        {
            try
            {
                await _db.Departments.AddAsync(department);
                await _db.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Add department failed ");
                throw;
            }
        }
    }
}
