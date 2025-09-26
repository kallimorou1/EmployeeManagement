using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.Models
{
    /// <summary>
    /// Represents an employee entity with identifying and work-related details.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// identifier
        /// </summary>
        #region EMPLOYEE ID
        public int Id { get; set; }
        #endregion

        #region EMPLOYEE NAME
        [StringLength(50, MinimumLength =1, ErrorMessage = "Name cannot be empty.")]
        public required string Name { get; set; }
        #endregion

        #region EMPLOYEE POSITION
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Position cannot be empty.")]
        public required string Position { get; set; }
        #endregion

        #region EMPLOYEE DEPARTMENT
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Department cannot be empty.")]
        public required string Department { get; set; }
        #endregion

        #region EMPLOYEE SALARY
        [RegularExpression(@"^(((\d{1,3})(,\d{3})*)|(\d+))(.\d+)?$", ErrorMessage = "Salary must be a non-negative number and can include commas as thousand separators.")]
        [Range(0, int.MaxValue)]
        public decimal Salary { get; set; }
        #endregion
    }
    #region EMPLOYEE DATA
    /// <summary>
    /// A predefined list of employees with example data.
    /// </summary>
    public static class EmployeeData
    {
        public static List<Employee> Employees 
            = new List<Employee>
        {
            new Employee { Id = 1, Name = "John Doe", Position = "Software Engineer", Department = "IT", Salary = 60000 },
            new Employee { Id = 2, Name = "Jane Smith", Position = "Project Manager", Department = "IT", Salary = 75000 },
            new Employee { Id = 3, Name = "Sam Brown", Position = "HR Specialist", Department = "HR", Salary = 50000 }
        };
    }
    #endregion

}
