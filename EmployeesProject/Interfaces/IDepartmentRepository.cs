using EmployeesProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesProject.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAll();
        Task Add(Department department);     
    }
}
