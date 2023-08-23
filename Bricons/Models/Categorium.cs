using System.ComponentModel.DataAnnotations;

namespace Bricons.Models
{
    public class Categorium
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la categoria es obligatorio")]
        [Display(Name = "Categoria")]

        public string? NombreCategoria { get; set; }

        public virtual ICollection<Producto>? Productos { get; set; } = new List<Producto>();
    }
}
