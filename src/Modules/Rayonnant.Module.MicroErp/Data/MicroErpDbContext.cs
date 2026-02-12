using Microsoft.EntityFrameworkCore;
using Rayonnant.Module.MicroErp.Entities;

namespace Rayonnant.Module.MicroErp.Data;

public class MicroErpDbContext : DbContext
{
    public MicroErpDbContext(DbContextOptions<MicroErpDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Pcb> Pcbs { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Guidebook> Guidebooks { get; set; }
    public DbSet<Employee> Employees { get; set; }
    
    // Lookup tables
    public DbSet<SolderColor> SolderColors { get; set; }
    public DbSet<OverlayColor> OverlayColors { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<CoverType> CoverTypes { get; set; }
    public DbSet<AssemblyType> AssemblyTypes { get; set; }
    public DbSet<AssemblyTechnologyType> AssemblyTechnologyTypes { get; set; }
    public DbSet<OrderType> OrderTypes { get; set; }
    public DbSet<OrderDeliveryType> OrderDeliveryTypes { get; set; }
    public DbSet<ComponentsDeliveryType> ComponentsDeliveryTypes { get; set; }
    public DbSet<DocumentationType> DocumentationTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships
        modelBuilder.Entity<Pcb>()
            .HasOne(p => p.Customer)
            .WithMany(c => c.Pcbs)
            .HasForeignKey(p => p.CustomerId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Pcb)
            .WithMany(p => p.Orders)
            .HasForeignKey(o => o.PcbId);

        modelBuilder.Entity<Guidebook>()
            .HasOne(g => g.Order)
            .WithMany(o => o.Guidebooks)
            .HasForeignKey(g => g.OrderId);

        // Configure optional relationships
        ConfigureOptionalRelationships(modelBuilder);
    }

    private void ConfigureOptionalRelationships(ModelBuilder modelBuilder)
    {
        // Customer relationships
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.DefaultDeliveryType)
            .WithMany()
            .HasForeignKey(c => c.DefaultDeliveryTypeId)
            .OnDelete(DeleteBehavior.SetNull);

        // PCB relationships
        modelBuilder.Entity<Pcb>()
            .HasOne(p => p.Material)
            .WithMany()
            .HasForeignKey(p => p.MaterialId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Pcb>()
            .HasOne(p => p.DefaultTopSolder)
            .WithMany()
            .HasForeignKey(p => p.DefaultTopSolderId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Pcb>()
            .HasOne(p => p.DefaultBottomSolder)
            .WithMany()
            .HasForeignKey(p => p.DefaultBottomSolderId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Pcb>()
            .HasOne(p => p.DefaultTopOverlay)
            .WithMany()
            .HasForeignKey(p => p.DefaultTopOverlayId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Pcb>()
            .HasOne(p => p.DefaultBottomOverlay)
            .WithMany()
            .HasForeignKey(p => p.DefaultBottomOverlayId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Pcb>()
            .HasOne(p => p.Cover)
            .WithMany()
            .HasForeignKey(p => p.CoverId)
            .OnDelete(DeleteBehavior.SetNull);

        // Order relationships - all the foreign key relationships
        ConfigureOrderRelationships(modelBuilder);
    }

    private void ConfigureOrderRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Documentation)
            .WithMany()
            .HasForeignKey(o => o.DocumentationId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.SolderLayerTop)
            .WithMany()
            .HasForeignKey(o => o.SolderLayerTopId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.SolderLayerBottom)
            .WithMany()
            .HasForeignKey(o => o.SolderLayerBottomId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.OverlayLayerTop)
            .WithMany()
            .HasForeignKey(o => o.OverlayLayerTopId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.OverlayLayerBottom)
            .WithMany()
            .HasForeignKey(o => o.OverlayLayerBottomId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.CoverType)
            .WithMany()
            .HasForeignKey(o => o.CoverTypeId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.OrderType)
            .WithMany()
            .HasForeignKey(o => o.OrderTypeId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Employee)
            .WithMany()
            .HasForeignKey(o => o.EmployeeId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Material)
            .WithMany()
            .HasForeignKey(o => o.MaterialId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}