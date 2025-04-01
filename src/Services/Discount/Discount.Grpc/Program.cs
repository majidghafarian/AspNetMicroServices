﻿
using Discount.Grpc.Repositories;
using Discount.Grpc.Repostories;
using Discount.Grpc.Extension;
using Discount.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// استفاده از AddSingleton یا AddScoped برای تزریق وابستگی
builder.Services.AddSingleton<IDiscountRepository>(new DiscountRepository(connectionString));



// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();


// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.MigrationDatabase<Program>();
app.Run();
