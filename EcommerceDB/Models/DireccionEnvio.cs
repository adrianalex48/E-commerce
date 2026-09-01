using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceDB.Models
{
    [Table("direccionesenvio")]
    public class DireccionEnvio
    {
        [Column("id")] public int Id { get; set; }
        [Column("clienteid")] public int ClienteId { get; set; }
        [Column("direccionfisica")] public string DireccionFisica { get; set; } = string.Empty;
        [Column("espredeterminada")] public bool EsPredeterminada { get; set; }
    }
}