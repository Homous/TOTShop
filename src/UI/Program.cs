using Application;
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
    //builder.Logging.AddSerilog(logger);

    Log.Logger = logger;
    // Add services to the container.
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    //builder.Services.AddControllers();
    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    /* builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
     builder.Services.AddScoped(typeof(IPipelineBehavior<,>),
         typeof(SerilogBehavior<,>));*/

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
}
catch (Exception ex)
{
    Log.Fatal(ex, "An exception has occured");
}
finally
{
    Log.CloseAndFlush();
}