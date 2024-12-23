using System.Configuration;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Library.Web;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Services.Services.Notifications;
using Scalar.AspNetCore;
using Serilog;
using Stripe;
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

using(var scope=app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var BorrowedBookNotificationService = services.GetRequiredService<IBorrowedBookNotificationServices>();
    var recurringJobManager = services.GetRequiredService<IRecurringJobManager>();

    // Add or update the recurring job
    recurringJobManager.AddOrUpdate(
        "SendNotificationToBorrower",
        () => BorrowedBookNotificationService.SendNotificationToBorrower(),
        Cron.Daily);

    //var cartNotificationService = services.GetRequiredService<ICartNotificationServices>();
    //recurringJobManager.AddOrUpdate(
    //    "RemoveCompleted",
    //    () => cartNotificationService.RemoveCompletedAsync(),
    //    Cron.Daily);
}
app.UseStaticFiles();
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

StripeConfiguration.ApiKey = app.Configuration[$"{nameof(StripeSettings)}:SecretKey"];
app.Run();
