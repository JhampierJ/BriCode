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
        modelBuilder.Entity<PedidoProducto>()
         .HasKey(pp => new { pp.PedidoId, pp.ProductoId });

        modelBuilder.Entity<PedidoProducto>()
            .HasOne(pp => pp.Pedido)
            .WithMany(p => p.PedidoProductos)
            .HasForeignKey(pp => pp.PedidoId);

        modelBuilder.Entity<PedidoProducto>()
            .HasOne(pp => pp.Producto)
            .WithMany(p => p.PedidoProductos)
            .HasForeignKey(pp => pp.ProductoId);


        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Bricons.Models.Producto> Producto { get; set; } = default!;

    public DbSet<Bricons.Models.Usuario> Usuario { get; set; } = default!;

    public DbSet<Bricons.Models.Categorium> Categorium { get; set; } = default!;

    public DbSet<Bricons.Models.Pedido> Pedido { get; set; } = default!;

    public DbSet<Bricons.Models.Programacion> Programacion { get; set; } = default!;
    public DbSet<Bricons.Models.PedidoProducto> PedidoProducto { get; set; } = default!;

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
}
