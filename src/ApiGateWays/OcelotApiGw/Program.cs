using Microsoft.Extensions.Hosting.Internal;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ۱. تنظیم کانفیگ برای Ocelot
var env = builder.Environment;
builder.Configuration
    //.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("Ocelot.Development.json", optional: false, reloadOnChange: true);
    //.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// ۲. اضافه کردن سرویس Ocelot
builder.Services.AddOcelot().AddCacheManager(settings=>settings.WithDictionaryHandle());

// ۳. لاگینگ (اختیاری ولی بهتره)
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();
app.UseRouting();
 
await app.UseOcelot();
app.Run();
