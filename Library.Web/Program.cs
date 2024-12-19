using Hangfire;
using HangfireBasicAuthenticationFilter;
using Library.Web;
using Scalar.AspNetCore;
using Serilog;
var builder = WebApplication.CreateBuilder(args);


// logging
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)  
    .CreateLogger();
builder.Host.UseSerilog();
// Add services to the container.
builder.Services.AddControllers();
// cash data 
builder.Services.AddDistributedMemoryCache();
builder.Services.AddOpenApi();
builder.Services.ServicesInjection(builder.Configuration);

var app = builder.Build();
app.MapStaticAssets();
app.UseHangfireDashboard("/jobs", new DashboardOptions
{
    Authorization = new[]
    {
        new HangfireCustomBasicAuthenticationFilter
        {
            User="admin",
            Pass="admin"
        }
    }
});
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
