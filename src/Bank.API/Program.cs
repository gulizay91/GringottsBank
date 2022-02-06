using Bank.API.Extensions;
using FluentValidation.AspNetCore;
using MediatR;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var assembly = System.Reflection.Assembly.GetExecutingAssembly();

builder.Configuration
  .SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
  .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", false, true)
  .AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers()
    //.AddControllers(options =>
    //{
    //    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
    //})
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
    //.AddNewtonsoftJson(); // todo: for http patch
builder.Services
    .AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssembly(assembly);
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.EnableAnnotations(); });

builder.Services.AddLogging();
builder.Services.AddHealthChecks();
builder.Services.AddMediatR(assembly);

builder.Services.DIConfigureService(builder.Configuration); // register applicationServices

// Add Host configuration 
builder.Host.AddNServiceBusConfigurations();

// Wait 30 seconds for graceful shutdown.
builder.Host.ConfigureHostOptions(o => o.ShutdownTimeout = TimeSpan.FromSeconds(30));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();

    Console.WriteLine($"environment ASPNETCORE_ENVIRONMENT: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");
    Console.WriteLine($"config NSBTransportHost: {builder.Configuration.GetValue<string>("NSBTransportHost")}");
    Console.WriteLine($"config MssqlConnectionString: {builder.Configuration.GetValue<string>("MssqlConnectionString")}");
}


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHealthChecks("/hc");

app.Run();
