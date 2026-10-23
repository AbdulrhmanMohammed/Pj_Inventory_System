using Microsoft.EntityFrameworkCore;
using Pj_Inventory_System.API.Services.CartItemService;
using Pj_Inventory_System.API.Services.CartItemService.Base;
using Pj_Inventory_System.Application.Services;
using Pj_Inventory_System.Application.Services.Base;
using Pj_Inventory_System.Infrastructure.Data;
using Pj_Inventory_System.Infrastructure.Repositories;
using Pj_Inventory_System.Infrastructure.Repositories.Base;
using Pj_Inventory_System.Infrastructure.Repositories.CartItemRepository;

var builder = WebApplication.CreateBuilder(args);

// تسجيل قاعدة البيانات
builder.Services.AddDbContext<InventorySystemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<
    IOrderRepository,
    OrderRepository>();

builder.Services.AddScoped<
    IOrderService,
    OrderService>();



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
