using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using Newtonsoft;
using Npgsql;

namespace EmployeeManagement.Infrastructure
{
    public static class DbInstaller
    {
        public static IServiceCollection AddDBhandler(this IServiceCollection services, IConfiguration config)
        {
            DatabaseCredentials credentials = JsonConvert.DeserializeObject<DatabaseCredentials>(config["employee_db_connection"]);
            if(credentials == null ) { throw new ArgumentNullException("Connection styring cannot be null"); }


            NpgsqlDataSource datasrc = NpgsqlDataSource.Create(credentials?.ConnectionString);

            services.AddSingleton(datasrc);
            return services;

        }
    }
}
