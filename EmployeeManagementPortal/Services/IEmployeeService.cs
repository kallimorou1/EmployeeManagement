using EmployeeManagement.Shared.Models;

namespace EmployeeManagement.Portal.Services
{
    public interface IEmployeeService
    {
        Task<bool> Add(Employee newEmployee);
        Task<bool> DeleteById(int id);
        Task<(List<Employee> Employees, int TotalPages)> GetAll(PaginationDTO pagination);
        Task<Employee?> GetById(int id);
        Task<bool> UpdateEmployee(Employee updatedEmployee);
    }
}