using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceDB.Models
{
    [Table("proveedoresproductos")]
    [PrimaryKey(nameof(ProveedorId), nameof(ProductoId))]
    public class ProveedorProducto
    {
        [Column("proveedorid")] public int ProveedorId { get; set; }
        [Column("productoid")] public int ProductoId { get; set; }
        [Column("preciocompra")] public decimal PrecioCompra { get; set; }
        [Column("tiempoentregadias")] public int TiempoEntregaDias { get; set; }
    }
}