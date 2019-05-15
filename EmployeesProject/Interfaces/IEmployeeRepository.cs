using EmployeesProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesProject.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAll();
        Task Add(Employee employee);
        Task Update(Employee employee);
        Task<Employee> GetData(int id);
        Task Delete(int id);
        Task<Employee> ConfirmDelete(int id);
    }
}
