using EmployeeManagement.Shared.Models;

namespace EmployeeManagement.API.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAll();
        Task<Employee?> GetEmployeeById(int id);
        Task<Employee> Add(Employee employee);
        Task<Employee?> UpdateEmployee( Employee employee);
        Task<bool> DeleteById(int id);
    }
}
