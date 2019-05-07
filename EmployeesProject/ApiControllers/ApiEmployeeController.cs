using EmployeesProject.Controllers.Interfaces;
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
        private readonly IEmployeeDataAccessLayer _objemployee;

        public ApiEmployeeController(IEmployeeDataAccessLayer objemployee)
        {
            _objemployee = objemployee;
        }

        [HttpGet]
        [Route("api/Employee/Index")]
        public Task<IEnumerable<Employee>> Index()
        {
            return _objemployee.GetAllEmployees();
        }

        [HttpPost]
        [Route("api/Employee/Create")]
        public Task<int> Create([FromBody] Employee employee)
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
        public Task<int> Edit([FromBody]Employee employee)
        {
            return _objemployee.UpdateEmployee(employee);
        }

        [HttpGet]
        [Route("api/Employee/Delete/{id}")]
        public Task<Employee> ConfirmDelete(int id)
        {
            return _objemployee.ConfirmDelete(id);
        }

        [HttpDelete]
        [Route("api/Employee/Delete/{id}")]
        public Task<int> Delete(int id)
        {
            return _objemployee.DeleteEmployee(id);
        }

        [HttpGet]
        [Route("api/Employee/GetDepartmentList")]
        public async Task<IEnumerable<Department>> Details()
        {
            return await _objemployee.GetDepartments();
        }

        //[HttpPost]
        //[Route("api/Employee/AddDepartment")]
        //public Task<int> AddDepartment([FromBody] Employee employee)
        //{
        //    return _objemployee.AddEmployee(employee);
        //}
    }
}
