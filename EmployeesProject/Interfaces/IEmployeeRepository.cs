using EmployeesProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesProject.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task AddEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task<Employee> GetEmployeeData(int id);
        Task DeleteEmployee(int id);
        //Task<List<Department>> GetDepartments();
        Task<Employee> ConfirmDeleteEmployee(int id);
    }
}
