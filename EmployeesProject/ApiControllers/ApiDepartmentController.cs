using EmployeesProject.Interfaces;
using EmployeesProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesProject.ApiControllers
{
    [Authorize]
    public class ApiDepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;

        public ApiDepartmentController(IDepartmentRepository objemployee)
        {
            _departmentRepo = objemployee;
        }

        [HttpGet]
        [Route("api/Employee/GetDepartmentList")]
        public async Task<IEnumerable<Department>> Details()
        {
            return await _departmentRepo.GetDepartments();
        }

        [HttpPost]
        [Route("api/Department/AddDepartment")]
        public Task AddDepartment([FromBody] Department department)
        {
            return _departmentRepo.AddDepartment(department);
        }
    }
}