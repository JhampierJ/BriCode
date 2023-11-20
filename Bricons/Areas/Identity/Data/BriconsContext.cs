using Bricons.Areas.Identity.Data;
using Bricons.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Bricons.Models;
using System.Reflection.Emit;

namespace Bricons.Data;

public class BriconsContext : IdentityDbContext<ApplicationUser>
{
    public BriconsContext(DbContextOptions<BriconsContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CotizacionProducto>()
         .HasKey(pp => new { pp.CotizacionId, pp.ProductoId });

        modelBuilder.Entity<CotizacionProducto>()
            .HasOne(pp => pp.Cotizacion)
            .WithMany(p => p.CotizacionProductos)
            .HasForeignKey(pp => pp.CotizacionId);

        modelBuilder.Entity<CotizacionProducto>()
            .HasOne(pp => pp.Producto)
            .WithMany(p => p.CotizacionProductos)
            .HasForeignKey(pp => pp.ProductoId);


        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Bricons.Models.Producto> Producto { get; set; } = default!;

    public DbSet<Bricons.Models.Usuario> Usuario { get; set; } = default!;

    public DbSet<Bricons.Models.Categorium> Categorium { get; set; } = default!;

    public DbSet<Bricons.Models.Pedido> Pedido { get; set; } = default!;

    public DbSet<Bricons.Models.CotizacionProducto> CotizacionProducto { get; set; } = default!;

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<Bricons.Models.Cotizacion> Cotizacion { get; set; } = default!;
}
