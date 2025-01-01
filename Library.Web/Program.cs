using System.Configuration;
using APIWeaver;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Library.Web;
using Library.Web.ApiDocumentation;
using LibrarySystem.Domain.Abstractions;
using LibrarySystem.Domain.IRepository;
using LibrarySystem.Services.Services.Fines;
using LibrarySystem.Services.Services.Notifications;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
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
builder.Services.AddOpenApi( options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

builder.Services.ServicesInjection(builder.Configuration);



var app = builder.Build();

//using(var scope=app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var BorrowedBookNotificationService = services.GetRequiredService<IBorrowedBookNotificationServices>();
//    var fineNoteficationServices = services.GetRequiredService<IFineNotificationServices>();
//    var recurringJobManager = services.GetRequiredService<IRecurringJobManager>();

    
//    recurringJobManager.AddOrUpdate(
//        "AddFine",
//        () => fineNoteficationServices.AddFine(),
//        Cron.Daily);
//    recurringJobManager.AddOrUpdate(
//        "SendNotificationToBorrower",
//        () => BorrowedBookNotificationService.SendFineNotificationToBorrower(),
//        Cron.Daily);
//    recurringJobManager.AddOrUpdate(
//        "SendReminderNotificationToBorrower",
//        () => BorrowedBookNotificationService.SendReminderNotificationToBorrower(),
//        Cron.Daily);

//}
app.UseStaticFiles();
app.MapStaticAssets();
app.UseHangfireDashboard("/jobs");

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(option =>
    {
        option.Title = "Library System API";
        option.Theme = ScalarTheme.Mars;
    });
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

StripeConfiguration.ApiKey = app.Configuration[$"{nameof(StripeSettings)}:SecretKey"];
app.Run();
