using EmployeeManagement.Models;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Mvc;
using CSharpFunctionalExtensions;
using EmployeeManagement.Services;


namespace EmployeeManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
   private readonly IEmployeeService _empService;
    public EmployeeController(IEmployeeService empService) 
    {
        _empService = empService;
    }

//    private readonly ILogger<EmployeeController> _logger;

  


    [HttpPost(Name = "CreateEmployee")]
    public async Task<Result> CreateEmployee(CreateEmployeeRequest request) 
    {
        
       Result result =   await _empService.CreateRecord(request);
       return result;
       
    }

    [HttpGet(Name = "GetEmployee")]
    public async Task<EmployeeViewModel> GetEmployee(int empId)
    {
       
        Result<EmployeeViewModel> result = await _empService.GetEmployeeDetails(empId);

        return result.Value;
        
       
    }
}