using Hangfire;
using Library.Web;
using Library.Web.ApiDocumentation;
using LibrarySystem.Domain.Abstractions;
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapScalarApiReference(option =>
    {
        option.Title = "Library System API";
        option.Theme = ScalarTheme.Mars;
    });
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseRateLimiter();

StripeConfiguration.ApiKey = app.Configuration[$"{nameof(StripeSettings)}:SecretKey"];
app.Run();
