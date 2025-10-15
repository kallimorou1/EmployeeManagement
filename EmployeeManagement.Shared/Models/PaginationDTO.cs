namespace EmployeeManagement.Shared.Models
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;    
        public int QuantityPerPage { get; set; } = 10;

        public string? SearchTerm { get; set; } = null;

        public string? SortColumn { get; set; } = null;

        public SortOrder SortOrder { get; set; } = SortOrder.Ascending;

    }
}
