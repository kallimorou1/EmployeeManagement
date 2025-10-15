using Azure;
using EmployeeManagement.API.Data;
using EmployeeManagement.API.Helpers;
using EmployeeManagement.API.Services;
using EmployeeManagement.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace EmployeeManagement.API.Controllers

{
    [ApiController]
    [Route("api/[controller]")]

    /// <summary>
    /// Controller for managing employee-related operations.
    /// </summary>
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeContext _context;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(EmployeeContext context, IEmployeeService employeeService)
        {
            _context = context;
            _employeeService = employeeService;
        }
        #region GET ALL EMPLOYEES

        /// <summary>
        /// Retrieves a collection of all employees.
        /// </summary>
        /// <remarks>This method returns a 404 status code if no employees are found in the data
        /// source.</remarks>
        /// <returns>An <see cref="ActionResult{T}"/> containing an <see cref="IEnumerable{T}"/> of <see cref="Employee"/>
        /// objects if employees are available; otherwise, a 404 Not Found status code.</returns>
     

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.SearchTerm))
            { 
                var term = pagination.SearchTerm.ToLower();
                queryable = queryable.Where(e => 
                e.Name.ToLower().Contains(term) || 
                e.Department.ToLower().Contains(term) ||
                e.Position.ToLower().Contains(term)
                );
            }
       

            if (string.IsNullOrWhiteSpace(pagination.SortColumn))
            {
                pagination.SortColumn = "Id";
            }

            //Insert pagination parameters in the response headers
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.QuantityPerPage);
            //Applied pagination and return the data 
            var employees = (pagination.SortOrder == Shared.Models.SortOrder.Ascending) ?
                await queryable
                .OrderBy(p => EF.Property<Employee>(p, pagination.SortColumn))
                .Paginate(pagination)
                .ToListAsync()
            :
            await queryable
                .OrderByDescending(p => EF.Property<Employee>(p, pagination.SortColumn))
                .Paginate(pagination)
                .ToListAsync();

            return employees;
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
            var employee = await _employeeService.GetEmployeeById(id);
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
        [HttpPost("Add")]
        public async Task<ActionResult> Add([FromBody] Employee newEmployee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _employeeService.Add(newEmployee);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _employeeService.UpdateEmployee(employee);

            if (updated is null)return NotFound();
            return Ok(updated);
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
            var success = await _employeeService.DeleteById(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
       
        #endregion
    }
}
