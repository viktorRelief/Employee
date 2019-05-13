using EmployeesProject.Controllers.Interfaces;
using EmployeesProject.Interfaces;
using EmployeesProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeesProject.Controllers
{
    [Authorize]
    public class ApiEmployeeController : Controller
    {
        private readonly IEmployeeRepository _objemployee;
        private readonly IDepartmentDataAccessLayer _objdepartment;

        public ApiEmployeeController(IEmployeeRepository objemployee, IDepartmentDataAccessLayer objdepartment)
        {
            _objemployee = objemployee;
            _objdepartment = objdepartment;
        }

        [HttpGet]
        [Route("api/Employee/Index")]
        public Task<IEnumerable<Employee>> Index()
        {
            return _objemployee.GetAllEmployees();
        }

        [HttpPost]
        [Route("api/Employee/Create")]
        public Task Create([FromBody] Employee employee)
        {
            return _objemployee.AddEmployee(employee);
        }

        [HttpGet]
        [Route("api/Employee/Details/{id}")]
        public Task<Employee> Details(int id)
        {
            return _objemployee.GetEmployeeData(id);
        }

        [HttpPut]
        [Route("api/Employee/Edit")]
        public Task Edit([FromBody]Employee employee)
        {
            return _objemployee.UpdateEmployee(employee);
        }

        [HttpGet]
        [Route("api/Employee/Delete/{id}")]
        public Task<Employee> ConfirmDelete(int id)
        {
            return _objemployee.ConfirmDeleteEmployee(id);
        }

        [HttpDelete]
        [Route("api/Employee/Delete/{id}")]
        public Task Delete(int id)
        {
            return _objemployee.DeleteEmployee(id);
        }

        //[HttpGet]
        //[Route("api/Employee/GetDepartmentList")]
        //public async Task<IEnumerable<Department>> Details()
        //{
        //    return await _objemployee.GetDepartments();
        //}

        //[HttpPost]
        //[Route("api/Department/AddDepartment")]
        //public Task<int> AddDepartment([FromBody] Department department)
        //{
        //    return _objdepartment.AddDepartment(department);
        //}
    }
}
