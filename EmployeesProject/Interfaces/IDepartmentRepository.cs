using EmployeesProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesProject.Interfaces
{
    public interface IDepartmentRepository
    {
        Task AddDepartment(Department department);
        Task<List<Department>> GetDepartments();
    }
}
