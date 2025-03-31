using Discount.api.Context;
using Discount.api.Extension;
using Discount.api.Repositories;
using Discount.api.Repostories;

var builder = WebApplication.CreateBuilder(args);
 

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// استفاده از AddSingleton یا AddScoped برای تزریق وابستگی
builder.Services.AddSingleton<IDiscountRepository>(new DiscountRepository(connectionString));





// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddLogging();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MigrationDatabase<Program>();
app.UseAuthorization();

app.MapControllers();

app.Run();
