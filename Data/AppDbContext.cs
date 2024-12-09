using Microsoft.EntityFrameworkCore;
using InventarioVentas.Models;
namespace InventarioVentas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Producto> productos { get; set; }
        public DbSet<Cliente> cliente { get; set; }
        public DbSet<Venta> venta { get; set; }
        public DbSet<VentaProducto> VentaProductos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de relaciones si es necesario
            modelBuilder.Entity<Venta>()
                .HasMany(v => v.VentaProductos)
                .WithOne(vp => vp.Venta)
                .HasForeignKey(vp => vp.VentaId);

            modelBuilder.Entity<VentaProducto>()
                .HasKey(vp => new { vp.VentaId, vp.ProductoId }); // Clave compuesta si es necesario

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Venta>()
                .Property(v => v.Total)
                .HasPrecision(18, 2);



            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Ventas)
                .WithOne(v => v.Cliente)
                .HasForeignKey(v => v.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Cliente)
                .WithMany(c => c.Ventas)
                .HasForeignKey(v => v.ClienteId);





        }
    }
}
