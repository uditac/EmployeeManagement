using EmployeeManagement.Models;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Services;
using CSharpFunctionalExtensions;

namespace EmployeeManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _depService;
    public DepartmentController(IDepartmentService depService)
    {
        _depService = depService;
    }

    //    private readonly ILogger<EmployeeController> _logger;




    [HttpPost(Name = "CreateDepartment")]
    public async Task<Result> CreateDepartmeent(Department request)
    {

        Result result = await _depService.CreateRecord(request);
        return result;

    }
}