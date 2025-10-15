namespace EmployeeManagement.Portal.ViewModels
{
    public class ParentViewModel
    {
        public bool Initilized { get; set; } = false;
        public required string PageTitle { get; set; } 

        public string? GlobalMessage { get; set; }
        public string GlobalMessageClass { get; set; } = "";

    }

}
