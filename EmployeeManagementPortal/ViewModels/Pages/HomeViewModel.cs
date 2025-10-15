using EmployeeManagement.Portal.Pages;
using EmployeeManagement.Portal.Services;
using EmployeeManagement.Portal.ViewModels;
using EmployeeManagement.Shared.Models;
using Microsoft.AspNetCore.Components;


public class HomeViewModel : ParentViewModel
{
    private readonly IEmployeeService EmployeeService;
    public List<Employee> Employees = new();
    private readonly NavigationManager _navigation;
    public HomeViewModel(IEmployeeService employeeService, NavigationManager navigation)
    {
        _navigation = navigation;
        EmployeeService = employeeService;
        PageTitle = "Home";
    }

    public async Task LoadData()
    {
        try
        {
            var paginationDto = new PaginationDTO
            {
                Page = 1,
                QuantityPerPage = 1000,
            };

            var (employees, _) = await EmployeeService.GetAll(paginationDto);
            Employees = employees;
        }
        finally
        {
            Initilized = true;
            PageTitle = $"Dashboard - {Employees.Count} employees found";
        }
    }

    public void OnButtonClick()
    {
        _navigation.NavigateTo("/EmployeeManagement");
    }
}
