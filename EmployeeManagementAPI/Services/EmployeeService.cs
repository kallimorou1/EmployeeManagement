using EmployeeManagement.API.Data;
using EmployeeManagement.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeManagement.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeContext _context;

        public EmployeeService(EmployeeContext context)
        {
            _context = context;
        }
        #region GET ALL EMPLOYEES
        public async Task<List<Employee>> GetAll()
        {
            return await _context.Employees.ToListAsync();
        }
        #endregion

        #region GET EMPLOYEE BY ID
        public async Task<Employee?> GetEmployeeById(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        #endregion

        #region ADD EMPLOYEE
        public async Task<Employee> Add(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
        #endregion

        #region UPDATE EMPLOYEE
        public async Task<Employee?> UpdateEmployee(Employee employee)
        {
                    var existing = await _context.Employees.FindAsync(employee.Id);
                    if (existing == null)
                        return null;

                    _context.Entry(existing).CurrentValues.SetValues(employee);
                    await _context.SaveChangesAsync();
                    return employee;    
        }
        #endregion

        #region DELETE EMPLOYEE BY ID
        public async Task<bool> DeleteById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
