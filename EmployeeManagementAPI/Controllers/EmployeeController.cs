using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers

{
    [ApiController]
    [Route("[controller]")]

    /// <summary>
    /// Controller for managing employee-related operations.
    /// </summary>
    public class EmployeeController : ControllerBase
    {
        #region GET ALL EMPLOYEES
        
        [HttpGet]

        public async Task <ActionResult<IEnumerable<Employee>>> Get()
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
        [HttpGet(Name = "GetEmployeeById/{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById([FromQuery]int id)
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
        [HttpPost(Name = "CreateEmployee")]
        public async Task<ActionResult> CreateEmployee([FromBody] Employee newEmployee)
        {
            var result = EmployeeData.Employees.FirstOrDefault(e => e.Id == newEmployee.Id);
            if (result != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, "Employee with the same ID already exists.");
            }
            else
            {
                int newId = EmployeeData.Employees.Max(e => e.Id) + 1;
                newEmployee.Id = newId;
                EmployeeData.Employees.Add(newEmployee);

                return await Task.FromResult(StatusCode(StatusCodes.Status201Created, newEmployee));
            }
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
        [HttpPut(Name = "UpdateEmployee")]
        public async Task<ActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            var result = EmployeeData.Employees.FirstOrDefault(e => e.Id == employee.Id);
            if (employee == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);

            }
            else {
                
                return await Task.FromResult(StatusCode(StatusCodes.Status201Created));
            }

        }
        #endregion

        #region DELETE EMPLOYEE BY ID
        /// <summary>
        /// Deletes an employee by their ID.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns> return status code 404 if employee not found and 204 if deleted</returns>
        [HttpDelete(Name = "DeleteEmployee")]
        public async Task<ActionResult> DeleteEmployee([FromQuery] int id)
        {
            var employee = EmployeeData.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            else
                {
                EmployeeData.Employees.Remove(employee);
                return await Task.FromResult(StatusCode(StatusCodes.Status204NoContent));
            }
        }
        #endregion
    }
}
