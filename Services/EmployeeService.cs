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
    Task<Result> UpdateEmployeeDetails(int empId, string empName);

    Task<Result> DeleteEmployee(int empId);
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
            Console.WriteLine(ex.StackTrace!.ToString()); 
        }

        return result;
        
    }

     public async Task<Result> UpdateEmployeeDetails(int empId, string empName)
    {
        try
        { await _repository.UpdateEmployeeDetails(empId, empName);
            return Result.Success("emp updated");
        }
        catch (Exception ex) 
        { Console.WriteLine(ex.Message);
           return Result.Failure("emp updation failed");
        }

    }

    public async Task<Result> DeleteEmployee(int empId)
    {
        try
        {
            await _repository.DeleteEmployee(empId);
            return Result.Success("emp updated");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Result.Failure("emp updation failed");
        }

    }

}
