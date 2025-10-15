using EmployeeManagement.Portal.Services;
using EmployeeManagement.Portal.ViewModels;
using EmployeeManagement.Shared.Models;
using Microsoft.AspNetCore.Components;

public class EManagementViewModel : ParentViewModel
{

    // List to hold employee data
    public int currentPage = 1;
    public int TotalPagesQuantity = 1;
    public List<Employee> Employees = new();
    public string searchTerm = string.Empty;
    public string? message = null;
    public string messageClass = "";
    public string SortOrder = "asc";
    public string? SortColumn = "Id";

    public readonly NavigationManager _navigation;
    public readonly IEmployeeService _employeeService;
    public EmployeeManagement.Portal.Pages.EmployeeManagement Component { get; set; }

    // State for modal and employee ID to be deleted
    public bool isModalVisible = false;
    public int employeeIdToDelete = 0;

    public PaginationDTO paginationDto { get; set; } = new();

    public EManagementViewModel(NavigationManager navigation, IEmployeeService employeeService, EmployeeManagement.Portal.Pages.EmployeeManagement component) 
    { 
          _navigation = navigation;
          _employeeService = employeeService;
        Component = component;
    }

    public async Task OnInitialized()
    {
        try
        {
            paginationDto = new PaginationDTO
            {
                Page = currentPage,
                QuantityPerPage = 10,
                SearchTerm = searchTerm,
                SortColumn = SortColumn ?? "Id",
                SortOrder = SortOrder?.Equals("asc", StringComparison.OrdinalIgnoreCase) == true
                ? EmployeeManagement.Shared.Models.SortOrder.Ascending
                : EmployeeManagement.Shared.Models.SortOrder.Descending
            };
        }
        finally
        {
            Initilized = true;
        }
        await LoadEmployees(currentPage);

    }

    public async Task ApplyFilter()
    {
        currentPage = 1; //  Reset to first page after applying
        paginationDto.Page = 1;
        paginationDto.SearchTerm = searchTerm;
        await LoadEmployees(currentPage);
    }

    public async Task ClearFilter()
    {
        searchTerm = string.Empty;
        paginationDto.SearchTerm = null;
        SortColumn = "Id";
        SortOrder = "asc";
        paginationDto.SortColumn = SortColumn;
        paginationDto.SortOrder = EmployeeManagement.Shared.Models.SortOrder.Ascending;

        currentPage = 1; // Reset to first page after clearing
        paginationDto.Page = 1;
        await LoadEmployees(currentPage);
    }

    public async Task LoadEmployees(int page)
    {
        try
        {
            paginationDto.Page = page;
        paginationDto.SortColumn = SortColumn ?? "Id";
        paginationDto.SortOrder = SortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase)
            ? EmployeeManagement.Shared.Models.SortOrder.Ascending
            : EmployeeManagement.Shared.Models.SortOrder.Descending;

        var (employees, totalPages) = await _employeeService.GetAll(paginationDto);
        Employees = employees;
        TotalPagesQuantity = totalPages;
        currentPage = page;
    }
        finally
        {
            Component.Refresh();
        }
        //refresh page
    }


    public async Task SelectedPage(int page)
    {
        paginationDto.Page = page;
        await LoadEmployees(page);
    }

    #region Delete Button
    // Method to delete an employee by ID
    public async Task DeleteEmployee(int id)
    {

        try
        {

            var response = await _employeeService.DeleteById(id);


            if (response)
            {
                // Refresh the list to 1st page after deletion
                currentPage = 1;
                message = "Employee deleted successfully!";
                messageClass = "alert alert-success";
                await Task.Delay(2000);
                await OnInitialized();

            }
            else
            {
                message = "Error deleting employee";
                messageClass = "alert alert-danger";
            }
        }
        catch (HttpRequestException)
        {
            message = "Network error. Unable to connect to the server.";
            messageClass = "alert alert-danger";
        }
        catch (Exception ex)
        {
            message = $"Unexpected error: {ex.Message}";
            messageClass = "alert alert-danger";
        }
        finally
        {
            Component.Refresh();
        }


    }

    #endregion

    #region Naviation
    public void UpdateEmployee(int id)
    {
        _navigation.NavigateTo($"/edit/{id}");
    }

    public void CreateEmployee()
    {
        _navigation.NavigateTo("/create");
    }
    #endregion


    #region Modal Filter
    // Show the delete confirmation modal
    public void ShowDeleteModal(int id)
    {
        employeeIdToDelete = id;  // Store the ID of the employee to delete
        isModalVisible = true; // Show the modal
    }

    // Hide the delete confirmation modal
    public void HideDeleteModal()
    {
        isModalVisible = false;  // Hide the modal
    }

    // Confirm delete and call DeleteEmployee
    public async Task ConfirmDeleteEmployee()
    {
        await DeleteEmployee(employeeIdToDelete);  // Delete the employee
        HideDeleteModal();  // Close the modal after deletion
    }

}
#endregion
