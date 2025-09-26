using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers

{
    [ApiController]
    [Route("api/[controller]")]

    /// <summary>
    /// Controller for managing employee-related operations.
    /// </summary>
    public class EmployeeController : ControllerBase
    {

        #region GET ALL EMPLOYEES

        /// <summary>
        /// Retrieves a collection of all employees.
        /// </summary>
        /// <remarks>This method returns a 404 status code if no employees are found in the data
        /// source.</remarks>
        /// <returns>An <see cref="ActionResult{T}"/> containing an <see cref="IEnumerable{T}"/> of <see cref="Employee"/>
        /// objects if employees are available; otherwise, a 404 Not Found status code.</returns>
        [HttpGet("GetAll")]

        public async Task <ActionResult<IEnumerable<Employee>>> GetAll()
        {
            if (EmployeeData.Employees == null || EmployeeData.Employees.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return await Task.FromResult(Ok(EmployeeData.Employees));
        }
        #endregion

        #region GET EMPLOYEE BY ID

        /// <summary>
        /// Gets an employee by their ID.
        /// </summary>
        /// <param name="id"> identifier for employee</param>
        /// <returns> returns the employee with the specified ID. </returns>
        [HttpGet("GetById")]
        public async Task<ActionResult<Employee>> GetById([FromQuery] int id)
        {
            var employee = EmployeeData.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return await Task.FromResult(employee);
        }
        #endregion

        #region  CREATE NEW EMPLOYEE
        /// <summary>
        /// Checks if id is already exists and creates new employee to Employee list with new unique ID.
        /// </summary>
        /// <param name=""></param>
        /// <returns> returns employee and 201 status code </returns>
        [HttpPost( "Add" )]
        public async Task<ActionResult> Add([FromBody] Employee newEmployee)
        {
            newEmployee.Id = EmployeeData.Employees.Max(e => e.Id) + 1;
            EmployeeData.Employees.Add(newEmployee);
            return await Task.FromResult(StatusCode(StatusCodes.Status201Created, newEmployee));
        }
        #endregion

        #region UPDATE EMPLOYEE DETAILS
        /// <summary>
        /// Updates the details of an existing employee.
        /// </summary>
        /// <param name="employee">The employee object containing updated information by ID. The object must not be null and must include valid
        /// employee details.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation. Returns a 404 status code if the
        /// employee is not found.</returns>
        [HttpPut("Update")]
        public async Task<ActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            var result = EmployeeData.Employees.FirstOrDefault(e => e.Id == employee.Id);
            if (result != null)
            {
                result.Name = employee.Name;
                result.Position = employee.Position;
                result.Department = employee.Department;
                result.Salary = employee.Salary;
            }
            return result == null ?  
                StatusCode(StatusCodes.Status404NotFound) : 
                await Task.FromResult(StatusCode(StatusCodes.Status204NoContent));           
        }
        #endregion

        #region DELETE EMPLOYEE BY ID
        /// <summary>
        /// Deletes an employee by their ID.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns> return status code 404 if employee not found and 204 if deleted</returns>
        [HttpDelete("DeleteById")]
        public async Task<ActionResult> DeleteById([FromQuery] int id)
        {
            var employee = EmployeeData.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                return StatusCode(StatusCodes.Status404NotFound);
            else
            {
                EmployeeData.Employees.Remove(employee);
                return await Task.FromResult(StatusCode(StatusCodes.Status204NoContent));
            }
        }
        #endregion
    }
}
