using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceDB.Models
{
    [Table("bodegas")]
    public class Bodega
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("ubicacion")]
        public string Ubicacion { get; set; } = string.Empty;

        [Required]
        [Column("capacidad")]
        public int Capacidad { get; set; }
    }
}