using FluentMigrator.Builders.Create.Table;
using Npgsql;


namespace EmployeeManagement.Infrastructure
{
    public static class Init
    {

        public static string CreateEmployeeTable = @"CREATE TABLE IF NOT EXISTS EMPLOYEES 
       (
        EMPLOYEE_ID serial PRIMARY KEY,
	    EMPLOYEE_NAME VARCHAR ( 50 ) UNIQUE NOT NULL,
	    DEPARTMENTID INT NOT NULL
	
       );";


        public static string CreateDepartmentTable = @"CREATE TABLE IF NOT EXISTS DEPARTMENT
       (
        DEPARTMENT_ID serial PRIMARY KEY,
	    DEPARTMENT_NAME VARCHAR ( 50 ) UNIQUE NOT NULL,
	    EMPLOYEEID INT NOT NULL
	
       );";

        public static async Task RunMigrations(NpgsqlDataSource dataSource)
        {

            NpgsqlConnection connection = await dataSource.OpenConnectionAsync();
            NpgsqlTransaction transaction = await connection.BeginTransactionAsync();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(CreateEmployeeTable, connection, transaction);
                await command.ExecuteNonQueryAsync();

                command = new NpgsqlCommand(CreateDepartmentTable, connection, transaction);
                await command.ExecuteNonQueryAsync();
            }
          catch (Exception ex) { 
              await  transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }
            finally { 
                await transaction.CommitAsync();
                await connection.CloseAsync();
            }  

        }
    }
}
