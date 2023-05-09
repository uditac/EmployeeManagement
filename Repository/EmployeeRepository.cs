using CSharpFunctionalExtensions;
using EmployeeManagement.Infrastructure;
using EmployeeManagement.Models;
using EmployeeManagement.Services;
using FluentMigrator.Runner.Processors;
using Npgsql;
using System.Data;
using System.IO;

namespace EmployeeManagement.Repository;



public interface IEmployeeRepository
{
    Task CreateEmployee(Employee employee);
    Task<EmployeeViewModel> GetEmployeeDetails(int empId);

    Task UpdateEmployeeDetails(int empId, string empName );

    Task DeleteEmployee(int empId);

}

public class EmployeeRepository : IEmployeeRepository
{
    NpgsqlDataSource _dataSource;
   
    public EmployeeRepository(NpgsqlDataSource dataSource)
    { 
       _dataSource = dataSource;
       
    
    }

    public  async Task CreateEmployee(Employee employee)
    {
        string cmd = $"INSERT INTO public.employee(empname, departmentid) VALUES(@empname,@departmentId)";
        NpgsqlConnection connection = await _dataSource.OpenConnectionAsync();
        NpgsqlTransaction transaction = await connection.BeginTransactionAsync();
        try
        {
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection, transaction);
            command.Parameters.Add(new NpgsqlParameter("empname",employee.EmployeeName));
           
            command.Parameters.Add(new NpgsqlParameter("departmentid", employee.DepartmentId));
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

    public async Task<EmployeeViewModel> GetEmployeeDetails(int empId)
    {
        EmployeeViewModel employeeViewModel =null!;
        string cmd = $"SELECT a.empid, a.empname, a.departmentid,b.department_name FROM public.employee a inner join public.department b on a.empid = b.employeeid and a.empid = (@empId)";
        NpgsqlConnection connection = await _dataSource.OpenConnectionAsync();
        NpgsqlTransaction transaction = await connection.BeginTransactionAsync();
        try
        {
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection, transaction);
            command.Parameters.Add(new NpgsqlParameter("empId", empId));
            NpgsqlDataReader dr = command.ExecuteReader();
            if(dr.HasRows) 
                {
                while (dr.Read())
                {
                    employeeViewModel = (new EmployeeViewModel()
                    {
                        EmployeeId = dr.GetInt32(0),
                        EmployeeName = dr.GetString(1),
                        DepartmentId = dr.GetInt32(2),
                        DepartmentName = dr.GetString(3)
                    }); ;
                }
            }
           employeeViewModel.DepartmentId = employeeViewModel.EmployeeId;
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

        return employeeViewModel!;
    }

    public async Task UpdateEmployeeDetails(int empId,string empName)
    {
        string cmd = $"UPDATE employee SET empname = (@empName) WHERE EXISTS (SELECT empname FROM employee WHERE empId = (@empId) )";
        NpgsqlConnection connection = await _dataSource.OpenConnectionAsync();
        NpgsqlTransaction transaction = await connection.BeginTransactionAsync();
        try
        {
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection, transaction);
            command.Parameters.Add(new NpgsqlParameter("empname", empName));

            command.Parameters.Add(new NpgsqlParameter("empId",empId));
            await command.ExecuteNonQueryAsync();

        }
        catch (Exception ex)
        {
            throw new Exception("Record already exixts");
            await transaction.RollbackAsync();
            throw new Exception(ex.Message);
        }
        finally
        {
            await transaction.CommitAsync();
            await connection.CloseAsync();
        }

    }

    public async Task DeleteEmployee(int empId)
    {
        string cmd = $"DELETE FROM employee WHERE empid = (@empId);";
        NpgsqlConnection connection = await _dataSource.OpenConnectionAsync();
        NpgsqlTransaction transaction = await connection.BeginTransactionAsync();
        try
        {
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection, transaction);
            command.Parameters.Add(new NpgsqlParameter("empId", empId));
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
