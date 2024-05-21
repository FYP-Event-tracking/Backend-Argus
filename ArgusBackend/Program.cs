using ArgusBackend;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

//Database Cotext Dependency Injection
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var userDbName = Environment.GetEnvironmentVariable("USER_DB_NAME");
var logDbName = Environment.GetEnvironmentVariable("LOG_DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var userConnectionString = $"Data Source={dbHost};Initial Catalog={userDbName};User ID=sa;Password={dbPassword};TrustServerCertificate=true;";
var logConnectionString = $"Data Source={dbHost};Initial Catalog={logDbName};User ID=sa;Password={dbPassword};TrustServerCertificate=true;";
builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(userConnectionString));
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
