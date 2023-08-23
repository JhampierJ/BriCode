using System.ComponentModel.DataAnnotations;

namespace Bricons.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "La categoria es obligatoria")]
        [Display(Name = "Categoria")]
        public int? CategoriaID { get; set; }
        [Required(ErrorMessage = "El nombre de producto es obligatoria")]
        [Display(Name = "Nombre del producto")]
        public string? NombreProducto { get; set; }
        [Required(ErrorMessage = "El stock es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El valor debe ser mayor o igual a cero.")]
        public int? Stock { get; set; }

        public virtual Categorium? Categoria { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

        public virtual ICollection<Programacion> Programaciones { get; set; } = new List<Programacion>();
    }
}
