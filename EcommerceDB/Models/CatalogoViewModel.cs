namespace EcommerceDB.Models
{
    public class CatalogoViewModel
    {
        public IEnumerable<Product> Productos { get; set; } = Enumerable.Empty<Product>();
        public IEnumerable<Categoria> Categorias { get; set; } = Enumerable.Empty<Categoria>();
    }
}