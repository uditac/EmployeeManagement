using CSharpFunctionalExtensions;
using EmployeeManagement.Infrastructure;
using EmployeeManagement.Models;
using EmployeeManagement.Services;
using Npgsql;

namespace EmployeeManagement.Repository;



public interface IDepartmentRepository
{
    Task CreateDepartment(Department department);


}

public class DepartmentRepository : IDepartmentRepository
{
    NpgsqlDataSource _dataSource;

    public DepartmentRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;


    }

    public async Task CreateDepartment(Department department)
    {
        string cmd = $"INSERT INTO public.department(department_name,EmployeeId) VALUES (@departmentname,@employeeid);";
        NpgsqlConnection connection = await _dataSource.OpenConnectionAsync();
        NpgsqlTransaction transaction = await connection.BeginTransactionAsync();
        try
        {
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection, transaction);
            command.Parameters.Add(new NpgsqlParameter("departmentname", department.DepartmentName));

            command.Parameters.Add(new NpgsqlParameter("employeeid", department.EmployeeId));
            await command.ExecuteNonQueryAsync();

        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception(ex.Message);
        }
        finally
        {
            await transaction.CommitAsync();
            await connection.CloseAsync();
        }


    }

}
