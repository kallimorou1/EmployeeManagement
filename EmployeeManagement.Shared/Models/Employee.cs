using System.ComponentModel.DataAnnotations;


namespace EmployeeManagement.Shared.Models
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
        [Range(typeof(decimal), "0", "999999999999.99", ErrorMessage = "Salary must be between 0 and 999,999,999,999.99")]
        public decimal Salary { get; set; }
        #endregion
    }

}
