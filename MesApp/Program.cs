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

// Configure database based on environment
var environment = builder.Environment.EnvironmentName;
var dbConfig = builder.Configuration.GetSection("Database");

string provider;
string connectionString;

if (environment == "Production")
{
    // Production: use appsettings.Production.json configuration
    provider = dbConfig["Provider"] ?? "Postgres";
    connectionString = dbConfig["ConnectionString"] ?? throw new InvalidOperationException("Production ConnectionString is required");
}
else
{
    // Development: use SQLite by default
    provider = dbConfig["Provider"] ?? "Sqlite";
    connectionString = dbConfig["ConnectionString"] ?? "Data Source=mes_dev.db";
}

// Use DbContextFactory instead of DbContext for Blazor Server stability
builder.Services.AddDbContextFactory<AppDbContext>(options =>
{
    switch (provider)
    {
        case "SqlServer":
            options.UseSqlServer(connectionString);
            break;
        case "Postgres":
            options.UseNpgsql(connectionString);
            break;
        default:
            options.UseSqlite(connectionString);
            break;
    }
});

var app = builder.Build();

// Apply migrations and seed data on startup using factory
using (var scope = app.Services.CreateScope())
{
    var dbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
    using var db = dbFactory.CreateDbContext();

    db.Database.Migrate();

    // Seed initial data if database is empty
    if (!db.Items.Any())
    {
        SeedData.EnsureSeed(db);
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