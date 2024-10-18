using FridgeManagement.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FridgeManagement.Models;

namespace FridgeManagement.Data;

public class AuthenticationContext : IdentityDbContext<FridgeManagementUser>
{
    public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    // DbSets for each table
    public DbSet<Location> Locations { get; set; }
    public DbSet<ServiceCheckup> ServiceCheckups { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<CustomerLiaison> CustomerLiaisons { get; set; }
    public DbSet<Fridge> Fridges { get; set; }
    public DbSet<FridgeAction> FridgeActions { get; set; }
    public DbSet<FridgeAllocation> FridgeAllocations { get; set; }
    public DbSet<PurchaseRequest> PurchaseRequests { get; set; }
    public DbSet<StockController> StockControllers { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Suburb> Suburbs { get; set; }
    public DbSet<Fault> Faults { get; set; }
    public DbSet<FaultTechnician> FaultTechnicians { get; set; }
    public DbSet<FaultReport> FaultReports { get; set; }
    public DbSet<ServiceHistory> ServiceHistories { get; set; }
    public DbSet<ServiceVisit> ServiceVisits { get; set; }
}
