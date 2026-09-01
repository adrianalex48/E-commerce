using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceDB.Models
{
    [Table("stockbodegas")]
    [PrimaryKey(nameof(BodegaId), nameof(ProductoId))]
    public class StockBodega
    {
        [Column("bodegaid")] public int BodegaId { get; set; }
        [Column("productoid")] public int ProductoId { get; set; }
        [Column("cantidad")] public int Cantidad { get; set; }
    }
}