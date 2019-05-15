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
        [Route("api/Department/GetAll")]
        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _departmentRepo.GetAll();
        }

        [HttpPost]
        [Route("api/Department/Add")]
        public Task Add([FromBody] Department department)
        {
            return _departmentRepo.Add(department);
        }
    }
}