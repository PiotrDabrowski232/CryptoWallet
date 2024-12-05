using CryptoWallet.Data.DatabaseConnection;
using CryptoWallet.DiConfig;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(
    fv =>
    {
        fv.RegisterValidatorsFromAssembly(Assembly.Load("CryptoWallet.Logic"));
    });

builder.Services.WithServices();

//DbContext
var connectionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<CryptoWalletDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
