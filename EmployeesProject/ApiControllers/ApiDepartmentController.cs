using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeesProject.Interfaces;
using EmployeesProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesProject.ApiControllers
{
    [Authorize]
    public class ApiDepartmentController : Controller
    {
        private readonly IDepartmentRepository _objdepartment;

        public ApiDepartmentController(IDepartmentRepository objemployee)
        {
            _objdepartment = objemployee;
        }

        [HttpGet]
        [Route("api/Employee/GetDepartmentList")]
        public async Task<IEnumerable<Department>> Details()
        {
            return await _objdepartment.GetDepartments();
        }

        [HttpPost]
        [Route("api/Department/AddDepartment")]
        public Task AddDepartment([FromBody] Department department)
        {
            return _objdepartment.AddDepartment(department);
        }
    }
}