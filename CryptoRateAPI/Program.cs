using CryptoRateAPI.Interfaces;
using CryptoRateAPI.Middlewares;
using CryptoRateAPI.Services;
using NLog;
using NLog.Web;


var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
try
{

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddScoped<ICryptoService, CryptoService>();
    builder.Services.AddScoped<IRequestServices, RequestServices>();



    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();


    app.UseMiddleware<ExceptionMiddleware>();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of an exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}
