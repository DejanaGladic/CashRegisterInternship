using Microsoft.OpenApi.Models;
using CashRegister.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using CashRegister.Domain.Interfaces;
using CashRegister.Infrastructure.Repositories;
using CashRegister.Application.ServiceInterfaces;
using CashRegister.Application.Services;
using System.Reflection;
using MediatR;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(options =>
{
    // Validate child properties and root collection elements
    options.ImplicitlyValidateChildProperties = true;
    options.ImplicitlyValidateRootCollectionElements = true;

    // Automatic registration of validators in assembly
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CashRegister.API", Version = "v1" });
});

builder.Services.AddDbContext < CashRegisterDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CashRegisterDBConnection")
    ));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBillService, BillService>();
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IProductBillService, ProductBillService>();
builder.Services.AddScoped<ICalculator, Calculator>();

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<IProductBillRepository, ProductBillRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
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
