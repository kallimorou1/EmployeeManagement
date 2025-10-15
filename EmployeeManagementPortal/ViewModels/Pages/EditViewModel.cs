using EmployeeManagement.Portal.Pages;
using EmployeeManagement.Portal.Services;
using EmployeeManagement.Portal.ViewModels;
using EmployeeManagement.Shared.Models;
using Microsoft.AspNetCore.Components;

public class EditViewModel : ParentViewModel
{
    private readonly NavigationManager _navigation;
    private readonly IEmployeeService _employeeService;

    public Employee? employee = null;

    public string? message = null;
    public string messageClass = "";
    public Edit Component { get; set; }


    public EditViewModel(NavigationManager navigation, IEmployeeService employeeService, Edit component)
    {
        _navigation = navigation;
        _employeeService = employeeService;
        Component = component;
    }

    public async Task OnInitialized(int id)
    {
        try
        {        
            employee = await _employeeService.GetById(id);
        }
        catch (Exception ex)
        {
            message = $"Failed to load employee: {ex.Message}";
            messageClass = "alert alert-danger";
        }
        finally
        {
            Initilized = true;
        }
        await Task.CompletedTask;
    }

    public async Task HandleValidSubmit()
    {
        try
        {
            if (employee is not null)

            {

                var response = await _employeeService.UpdateEmployee(employee);

                if (response)
                {
                    message = "Employee updated successfully!";
                    messageClass = "alert alert-success";
                    Component.Refresh();
                    await Task.Delay(2500);
                    _navigation.NavigateTo("/EmployeeManagement");
                }
                else
                {
                    message = "Failed to update employee";
                    messageClass = "alert alert-danger";

                }
            }
        }
        catch (HttpRequestException)
        {
            message = "Network error. Please check your connection.";
            messageClass = "alert alert-danger";
        }
        catch (Exception ex)
        {
            message = $"Error: {ex.Message}";
            Console.WriteLine($"Exception: {ex.Message}");
        }
        finally
        {
            Component.Refresh();
        }
    }

    public void EmployeeManagement()
    {
        _navigation.NavigateTo("/EmployeeManagement");
    }

}
