
using Serilog;
using Dapper;
using Npgsql;
using EmployeeManagement.Infrastructure;
using System.Reflection;

using var log = new LoggerConfiguration()//new
    .WriteTo.File("./logs.txt")
    .CreateLogger();

DefaultTypeMap.MatchNamesWithUnderscores = true;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
configuration.AddEnvironmentVariables()
    .AddUserSecrets(Assembly.GetExecutingAssembly());

builder.Services
    .AddSingleton<Serilog.ILogger>(log)
    .AddDBhandler(configuration);
log.Information("started logging");


// Add services to the container.

builder.Services.AddControllers();


IWebHostEnvironment environment = builder.Environment;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configuring dapper class

builder.Services.AddControllers();



var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await Init.RunMigrations(app.Services.GetRequiredService<NpgsqlDataSource>());


app.Run();
