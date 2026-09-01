using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceDB.Models
{
    [Table("productos")]
    public class Product
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("descripcion")]
        public string Descripcion { get; set; } = string.Empty;

        [Column("precioventa")]
        public decimal PrecioVenta { get; set; }

        [Column("categoriaid")]
        public int CategoriaId { get; set; }
        [Column("imagenurl")]
        public string ImagenUrl { get; set; } = string.Empty;

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }
    }
}