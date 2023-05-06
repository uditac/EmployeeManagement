using Dapper;
using Npgsql;
using System.Data;

using EmployeeManagement.Models;

using EmployeeManagement.Repository;
using CSharpFunctionalExtensions;

namespace EmployeeManagement.Services;

public interface IDepartmentService
{
    Task<Result> CreateRecord(Department request);
}


public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;


    public DepartmentService(IDepartmentRepository repository)
    {
        _repository = repository;
    }


    public async Task<Result> CreateRecord(Department request)
    {
        try
        {
            await _repository.CreateDepartment(new Department { DepartmentName = request.DepartmentName });
            return Result.Success("dep created");
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.Message);
        }

    }

}
