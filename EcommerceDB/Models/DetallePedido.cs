using System.ComponentModel.DataAnnotations.Schema;
namespace EcommerceDB.Models {
    [Table("detallespedido")]
    public class DetallePedido {
        [Column("id")] public int Id { get; set; }
        [Column("pedidoid")] public int PedidoId { get; set; }
        [Column("productoid")] public int ProductoId { get; set; }
        [Column("cantidad")] public int Cantidad { get; set; }
        [Column("preciomomentocompra")] public decimal PrecioMomentoCompra { get; set; }
    }
}