using Serilog;



using var log = new LoggerConfiguration()//new
    .WriteTo.File("./logs.txt")
    .CreateLogger();



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<Serilog.ILogger>(log);
log.Information("started logging");

// Add services to the container.

builder.Services.AddControllers();

ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
IWebHostEnvironment environment = builder.Environment;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
