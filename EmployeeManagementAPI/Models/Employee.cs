using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.Models
{
    /// <summary>
    /// Represents an employee entity with identifying and work-related details.
    /// Salary must be a non-negative number and can include commas as thousand separators.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// identifier
        /// </summary>
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Position { get; set; }
        public required string Department { get; set; }
        [RegularExpression(@"^(((\d{1,3})(,\d{3})*)|(\d+))(.\d+)?$", ErrorMessage = "Error")]
        [Range(0, int.MaxValue)]
        public decimal Salary { get; set; }

    }
    /// <summary>
    /// A predefined list of employees with example data.
    /// </summary>
    public static class EmployeeData
    {
        public static List<Employee> Employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John Doe", Position = "Software Engineer", Department = "IT", Salary = 60000 },
            new Employee { Id = 2, Name = "Jane Smith", Position = "Project Manager", Department = "IT", Salary = 75000 },
            new Employee { Id = 3, Name = "Sam Brown", Position = "HR Specialist", Department = "HR", Salary = 50000 }
        };
    }

}
