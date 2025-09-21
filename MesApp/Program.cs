using MesApp.Components;
using MesApp.Data;
using MesApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add CurrentUser service (scoped per circuit/session)
builder.Services.AddScoped<CurrentUser>();

// Add NotificationService
builder.Services.AddScoped<NotificationService>();

// Configure database based on configuration
var dbConfig = builder.Configuration.GetSection("Database");
var provider = dbConfig["Provider"] ?? "Sqlite";
var connectionString = dbConfig["ConnectionString"] ?? "Data Source=mes_dev.db";

// Use DbContextFactory instead of DbContext for Blazor Server stability
builder.Services.AddDbContextFactory<AppDbContext>(options =>
{
    switch (provider.ToLower())
    {
        case "sqlserver":
            options.UseSqlServer(connectionString);
            break;
        case "postgres":
        case "postgresql":
            options.UseNpgsql(connectionString);
            break;
        case "sqlite":
        default:
            options.UseSqlite(connectionString);
            break;
    }
});

var app = builder.Build();

// Apply migrations and seed data on startup using factory
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var dbFactory = services.GetRequiredService<IDbContextFactory<AppDbContext>>();

    try
    {
        using var db = dbFactory.CreateDbContext();
        db.Database.Migrate();
        logger.LogInformation("Database migrations applied successfully");

        // Seed initial data if database is empty
        if (!db.Items.Any())
        {
            SeedData.EnsureSeed(db);
            logger.LogInformation("Database seeded with initial data");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating or seeding the database");

        // For production, you might want to show a user-friendly message
        // For now, we'll log it and continue - the app should still work
        // even if migrations fail (though with potential issues)
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();