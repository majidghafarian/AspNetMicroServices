using Microsoft.EntityFrameworkCore;
using Ordering.Api.Extensions;
using Ordering.Application;
using Ordering.infrastructure;
using Ordering.infrastructure.Persistence;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddinfrastructureServices(builder.Configuration);

var app = builder.Build();
app.MigrationDataBase<OrderContext>((context, service) =>
{
    var logger = service.GetService<ILogger<OrderContextSeed>>();

    // ?? ????? ??????? ?? migrate ??
    context.Database.Migrate();

    // ?? ???? ?? ???? ???? seed ??
    OrderContextSeed.SeedAsync(context, logger).Wait();
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
