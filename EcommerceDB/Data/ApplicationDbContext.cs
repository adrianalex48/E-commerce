using EcommerceDB.Models;
using Microsoft.EntityFrameworkCore;

namespace Windbrands.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Bodega> Bodegas { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<StockBodega> StockBodegas { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<DetallePedido> DetallesPedido { get; set; }

        public DbSet<CuotaPago> CuotasPago { get; set; }
        public DbSet<DireccionEnvio> DireccionesEnvio { get; set; }
        public DbSet<IncidenciaDevolucion> IncidenciasDevolucion { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<ProveedorProducto> ProveedoresProductos { get; set; }
    }
}
