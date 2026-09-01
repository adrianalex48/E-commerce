using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceDB.Models
{
    [Table("proveedores")]
    public class Proveedor
    {
        [Column("id")] public int Id { get; set; }
        [Column("nombre")] public string Nombre { get; set; } = string.Empty;
    }
}