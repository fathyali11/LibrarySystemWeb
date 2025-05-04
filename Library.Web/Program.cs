using HealthChecks.UI.Client;
using Library.Web.ApiDocumentation;
using Library.Web;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Scalar.AspNetCore;
using Serilog;

try
{
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
    builder.Services.AddOpenApi(options =>
    {
        options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
    });
    builder.Services.AddEndpointsApiExplorer();



    builder.Services.AddSwaggerGen(options =>
    {
        var xmlDocsPath = $"{Directory.GetCurrentDirectory()}\\ExternalEndPointDocs";
        if (Directory.Exists(xmlDocsPath))
        {
            var xmlFiles = Directory.GetFiles(xmlDocsPath, "*.xml", SearchOption.TopDirectoryOnly);
            foreach (var xmlFile in xmlFiles)
            {
                options.IncludeXmlComments(xmlFile);
            }
        }
        else
        {
            Console.WriteLine($"XML documentation folder not found: {xmlDocsPath}");
        }
    });


    builder.Services.ServicesInjection(builder.Configuration);



    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var BorrowedBookNotificationService = services.GetRequiredService<IBorrowedBookNotificationServices>();
        var fineNoteficationServices = services.GetRequiredService<IFineServices>();
        var recurringJobManager = services.GetRequiredService<IRecurringJobManager>();
        var context = services.GetRequiredService<ApplicationDbContext>();

        // Apply any pending migrations
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }

        recurringJobManager.AddOrUpdate(
            "AddFine",
            () => fineNoteficationServices.AddFine(),
            Cron.Daily);
        recurringJobManager.AddOrUpdate(
            "SendNotificationToBorrower",
            () => BorrowedBookNotificationService.SendFineNotificationToBorrower(),
            Cron.Daily);
        recurringJobManager.AddOrUpdate(
            "SendReminderNotificationToBorrower",
            () => BorrowedBookNotificationService.SendReminderNotificationToBorrower(),
            Cron.Daily);

    }

    app.UseStaticFiles();
    app.MapStaticAssets();
    app.UseHangfireDashboard("/jobs");

    app.UseExceptionHandler();
    app.UseHttpsRedirection();
    app.UseSerilogRequestLogging();
 
        app.MapOpenApi();
        app.MapScalarApiReference(option =>
        {
            option.Title = "Library System API";
            option.Theme = ScalarTheme.Mars;
        });

        app.UseSwagger();
        app.UseSwaggerUI();

    
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.UseRateLimiter();
    app.UseHealthChecks("/health", new HealthCheckOptions
    {
        // UIResponseWriter => package: AspNetCore.HealthChecks.UI
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    app.UseHealthChecks("/health_api", new HealthCheckOptions
    {
        // UIResponseWriter => package: AspNetCore.HealthChecks.UI
        Predicate = _ => _.Tags.Contains("api"),
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    StripeConfiguration.ApiKey = app.Configuration[$"{nameof(StripeSettings)}:SecretKey"];
    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}
