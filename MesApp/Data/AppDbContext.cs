using Microsoft.EntityFrameworkCore;
using MesApp.Domain;

namespace MesApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Item> Items => Set<Item>();
    public DbSet<BusinessPartner> BusinessPartners => Set<BusinessPartner>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();
    public DbSet<MaterialReceipt> MaterialReceipts => Set<MaterialReceipt>();
    public DbSet<QcInspection> QcInspections => Set<QcInspection>();
    public DbSet<LabTestRequest> LabTestRequests => Set<LabTestRequest>();
    public DbSet<LabTestResult> LabTestResults => Set<LabTestResult>();
    public DbSet<PrepJob> PrepJobs => Set<PrepJob>();
    public DbSet<WorkCenter> WorkCenters => Set<WorkCenter>();
    public DbSet<RoutingOperation> RoutingOperations => Set<RoutingOperation>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
    public DbSet<IssueToProduction> IssueToProductions => Set<IssueToProduction>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}