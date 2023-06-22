using Application;
using Application.Middleware;
using Infrastructure;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

try
{
    Log.Information("Starting web application");

    var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

    builder.Logging.ClearProviders();
    builder.Host.UseSerilog(logger);

    Log.Logger = logger;
    // Add services to the container.
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseMiddleware<HttpRequestBodyMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "An exception has occured");
}
finally
{
    Log.CloseAndFlush();
}