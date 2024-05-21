using LogService_ArgusBackend;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

//Database Cotext Dependency Injection
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var logDbName = Environment.GetEnvironmentVariable("LOG_DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var logConnectionString = $"Data Source={dbHost};Initial Catalog={logDbName};User ID=sa;Password={dbPassword};TrustServerCertificate=true;";
builder.Services.AddDbContext<LogDbContext>(options => options.UseSqlServer(logConnectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
 