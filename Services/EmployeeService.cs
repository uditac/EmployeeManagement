using Dapper;
using Npgsql;
using System.Data;

using EmployeeManagement.Models;

using EmployeeManagement.Repository;
using CSharpFunctionalExtensions;

namespace EmployeeManagement.Services;

public interface IEmployeeService
{
    Task<Result> CreateRecord(CreateEmployeeRequest request);
    Task<Result<EmployeeViewModel>> GetEmployeeDetails(int empId);
}


public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

   
    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }


    public async Task<Result> CreateRecord(CreateEmployeeRequest request)
    {
        try
        {
            await _repository.CreateEmployee(new Employee { EmployeeName = request.FirstName + request.LastName, DepartmentId = request.DepartmentID });
            return Result.Success("emp created");
        }
        catch (Exception ex) {
            return Result.Failure(ex.Message);
        }
       
    }

    public async Task<Result<EmployeeViewModel>> GetEmployeeDetails(int EmpId)
    {
        Result<EmployeeViewModel> result = null;
        try
        {
            result = await _repository.GetEmployeeDetails(EmpId);
            return result;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace!.ToString()); ;
        }

        return result;
        
    }

}
