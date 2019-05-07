using EmployeesProject.Models;
using System.Threading.Tasks;

namespace EmployeesProject.Interfaces
{
    public interface IDepartmentDataAccessLayer
    {
        Task<int> AddDepartment(Department department);
    }
}
