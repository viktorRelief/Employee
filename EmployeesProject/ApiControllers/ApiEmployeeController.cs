using EmployeesProject.Interfaces;
using EmployeesProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesProject.Controllers
{
    [Authorize]
    public class ApiEmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepo;

        public ApiEmployeeController(IEmployeeRepository objemployee)
        {
            _employeeRepo = objemployee;
        }

        [HttpGet]
        [Route("api/Employee/Index")]
        public Task<IEnumerable<Employee>> Index()
        {
            return _employeeRepo.GetAllEmployees();
        }

        [HttpPost]
        [Route("api/Employee/Create")]
        public Task Create([FromBody] Employee employee)
        {
            return _employeeRepo.AddEmployee(employee);
        }

        [HttpGet]
        [Route("api/Employee/Details/{id}")]
        public Task<Employee> Details(int id)
        {
            return _employeeRepo.GetEmployeeData(id);
        }

        [HttpPut]
        [Route("api/Employee/Edit")]
        public Task Edit([FromBody]Employee employee)
        {
            return _employeeRepo.UpdateEmployee(employee);
        }

        [HttpGet]
        [Route("api/Employee/Delete/{id}")]
        public Task<Employee> ConfirmDelete(int id)
        {
            return _employeeRepo.ConfirmDeleteEmployee(id);
        }

        [HttpDelete]
        [Route("api/Employee/Delete/{id}")]
        public Task Delete(int id)
        {
            return _employeeRepo.DeleteEmployee(id);
        }
    }
}
