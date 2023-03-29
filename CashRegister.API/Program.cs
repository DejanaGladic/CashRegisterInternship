using Microsoft.OpenApi.Models;
using CashRegister.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using CashRegister.Domain.Interfaces;
using CashRegister.Infrastructure.Repositories;
using CashRegister.Application.ServiceInterfaces;
using CashRegister.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CashRegister.API", Version = "v1" });
});

builder.Services.AddDbContext<CashRegisterDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CashRegisterDBConnection")
    ));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<IProductBillRepository, ProductBillRepository>();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
