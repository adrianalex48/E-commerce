namespace EcommerceDB.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal PrecioVenta { get; set; }
        public int CategoriaId { get; set; }
    }
}
