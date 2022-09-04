using ex_dbtransactions.Database;
using ex_dbtransactions.Entities;
using ex_dbtransactions.UoW;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connString = builder.Configuration.GetConnectionString("DefaultConnection");
    var mySqlConnection = new MySqlConnection(connString);

    options.UseMySql(connString, ServerVersion.AutoDetect(mySqlConnection));
});

builder.Services.AddScoped<IRepository<Person, Guid>, Repository<Person, Guid>>();
builder.Services.AddScoped<IUnitOfWork<ApplicationDbContext>, UnitOfWork<ApplicationDbContext>>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();