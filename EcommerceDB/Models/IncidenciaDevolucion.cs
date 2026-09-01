using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceDB.Models
{
    [Table("incidenciasdevoluciones")]
    public class IncidenciaDevolucion
    {
        [Column("id")] public int Id { get; set; }
        [Column("bodegaid")] public int BodegaId { get; set; }
        [Column("productoid")] public int ProductoId { get; set; }
        [Column("fecha")] public DateTime Fecha { get; set; } = DateTime.Now;
        [Column("motivo")] public string Motivo { get; set; } = string.Empty;
        [Column("costo")] public decimal Costo { get; set; }
        [Column("creditogenerado")] public decimal CreditoGenerado { get; set; }
    }
}