using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ۱. تنظیم کانفیگ برای Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
// ۲. اضافه کردن سرویس Ocelot
builder.Services.AddOcelot();

// ۳. لاگینگ (اختیاری ولی بهتره)
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();
app.UseRouting();
 
await app.UseOcelot();
app.Run();
