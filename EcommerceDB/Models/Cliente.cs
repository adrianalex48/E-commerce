using System.ComponentModel.DataAnnotations.Schema;
namespace EcommerceDB.Models {
    [Table("clientes")]
    public class Cliente {
        [Column("id")] public int Id { get; set; }
        [Column("nombrecompleto")] public string NombreCompleto { get; set; } = string.Empty;
        [Column("correo")] public string Correo { get; set; } = string.Empty;
    }
}