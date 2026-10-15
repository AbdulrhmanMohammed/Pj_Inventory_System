using Microsoft.EntityFrameworkCore;
using Pj_Inventory_System.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// تسجيل قاعدة البيانات
builder.Services.AddDbContext<InventorySystemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
