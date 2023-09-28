using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required(ErrorMessage = "La imagen es obligatoria")]
        [Display(Name = "Imagen de producto")]
        public byte[] Imagen { get; set; }
        [Required(ErrorMessage = "La descripción es obligatoria")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El peso es obligatoria")]
        [Display(Name = "Peso")]
        [Range(typeof(decimal), "0.01", "9999.99", ErrorMessage = "El valor debe estar entre 0.01 y 9999.99")]
        public float Peso { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        [Display(Name = "Largo")]
        [Range(typeof(decimal), "0.01", "9999.99", ErrorMessage = "El valor debe estar entre 0.01 y 9999.99")]
        public float Largo { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        [Display(Name = "Ancho")]
        [Range(typeof(decimal), "0.01", "9999.99", ErrorMessage = "El valor debe estar entre 0.01 y 9999.99")]
        public float Ancho { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        [Range(typeof(decimal), "0.01", "9999.99", ErrorMessage = "El valor debe estar entre 0.01 y 9999.99")]
        [Display(Name = "Altura")]
        public float Altura { get; set; } 


        public virtual Categorium? Categoria { get; set; }

        //public float Calificacion { get; set; }
        //public float TotalCalificacion { get; set; }
        public ICollection<PedidoProducto> PedidoProductos { get; set; }
    }
}
