using CryptoWallet.Data.DatabaseConnection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//DbContext
var connectionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<CryptoWalletDbContext>(options => options.UseSqlServer(connectionString)
.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

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
