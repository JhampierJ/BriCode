using Bricons.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Bricons.Models;

namespace Bricons.Data;

public class BriconsContext : IdentityDbContext<ApplicationUser>
{
    public BriconsContext(DbContextOptions<BriconsContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Bricons.Models.Producto> Producto { get; set; } = default!;

    public DbSet<Bricons.Models.Usuario> Usuario { get; set; } = default!;

    public DbSet<Bricons.Models.Categorium> Categorium { get; set; } = default!;

    public DbSet<Bricons.Models.Pedido> Pedido { get; set; } = default!;

    public DbSet<Bricons.Models.Programacion> Programacion { get; set; } = default!;
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
}
