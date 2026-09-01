using System.ComponentModel.DataAnnotations.Schema;
namespace EcommerceDB.Models {
    [Table("categorias")]
    public class Categoria {
        [Column("id")]
        public int Id { get; set; }
        [Column("nombre")]
        public string Nombre { get; set; }
    }
}