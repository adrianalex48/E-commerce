using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace EcommerceDB.Models {
    [Table("cuotaspago")]
    public class CuotaPago {
        [Column("id")] public int Id { get; set; }
        [Column("pedidoid")] public int PedidoId { get; set; }
        [Column("monto")] public decimal Monto { get; set; }
        [Column("fecha")] public DateTime Fecha { get; set; } = DateTime.Now;
        [Column("metodopago")] public string MetodoPago { get; set; } = string.Empty;
        [Column("estado")] public string Estado { get; set; } = string.Empty;
    }
}