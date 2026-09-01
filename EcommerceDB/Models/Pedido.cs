using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceDB.Models
{
    [Table("pedidos")]
    public class Pedido
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("clienteid")]
        public int ClienteId { get; set; }

        [Column("bodegaorigenid")]
        public int BodegaOrigenId { get; set; }

        [Column("fechahora")]
        public DateTime FechaHora { get; set; } = DateTime.Now;

        [Required]
        [Column("estado")]
        public string Estado { get; set; } = "Pendiente";
    }
}