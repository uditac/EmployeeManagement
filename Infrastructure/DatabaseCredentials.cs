namespace EmployeeManagement.Infrastructure
{
    public class DatabaseCredentials
    {
        
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public string? Host { get; set; }

        public int Port { get; set; } = 5432;

        public string? Database { get; set; }

        public string? ConnectionString => $"User ID={UserName};Password={Password};Host={Host};Port={Port};Database={Database}; Integrated Security=true";      

    }
}
