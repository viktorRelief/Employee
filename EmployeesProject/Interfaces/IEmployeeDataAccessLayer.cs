using EmployeesProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesProject.Controllers.Interfaces
{
    public interface IEmployeeDataAccessLayer
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<int> AddEmployee(Employee employee);
        Task<int> UpdateEmployee(Employee employee);
        Task<Employee> GetEmployeeData(int id);
        Task<int> DeleteEmployee(int id);
        Task<List<Department>> GetDepartments();
        Task<Employee> ConfirmDelete(int id);
    }
}
