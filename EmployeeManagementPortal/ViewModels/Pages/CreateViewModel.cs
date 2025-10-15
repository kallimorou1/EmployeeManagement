using EmployeeManagement.Portal.Pages;
using EmployeeManagement.Portal.Services;
using EmployeeManagement.Portal.ViewModels;
using EmployeeManagement.Shared.Models;
using Microsoft.AspNetCore.Components;

public class CreateViewModel : ParentViewModel
{
    private readonly NavigationManager _navigation;
    private readonly IEmployeeService _employeeService;

    public Employee? Employee { get; set; }
    public string? Message { get; set; }
    public string MessageClass { get; set; } = "";
    public Create Component { get; set; }



    public CreateViewModel(NavigationManager navigation, IEmployeeService employeeService, Create component)
    {
        _navigation = navigation;
        _employeeService = employeeService;
        Component = component;
    }

    public Task OnInitialize()
    {
        try
        {
            Employee = new Employee
            {
                Name = "",
                Position = "",
                Department = ""
            };
        }
        finally
        {
            Initilized = true;
        }

        return Task.CompletedTask;
    }

    public async Task HandleValidSubmit()
    {
        try
        {
            if (Employee is not null)
            {
                var success = await _employeeService.Add(Employee);

                if (success)
                {
                    Message = "Employee created successfully!";
                    MessageClass = "alert alert-success";
                    Component.Refresh();
                    await Task.Delay(3000);
                    _navigation.NavigateTo("/EmployeeManagement");
                }
                else
                {
                    Message = "Failed to add employee.";
                    MessageClass = "alert alert-danger";
                }
            }
        }
        catch (HttpRequestException)
        {
            Message = "Network error. Please check your connection.";
            MessageClass = "alert alert-danger";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
        finally
        {
            Component.Refresh();
        }
    }

    public void NavigateToEmployeeList()
    {
        _navigation.NavigateTo("/EmployeeManagement");
    }
}
