using Bricons.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bricons.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El ID Usuario es obligatorio")]
        [Display(Name = "ID Usuario")]
        public int? UsuarioId { get; set; }
        [Required(ErrorMessage = "El ID Producto es obligatorio")]
        [Display(Name = "ID Producto")]
        public int? ProductoId { get; set; }
        [Required(ErrorMessage = "La fecha es obligatoria")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaEntrega { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Planta")]
        public string Sucursal { get; set; }
        [Required(ErrorMessage = "La dirección es obligatoria")]
        [Display(Name = "Direccion")]

        public string Pais {  get; set; }
        [Required(ErrorMessage = "La ciudad es obligatorio")]
        [Display(Name = "Ciudad")]
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El pais es obligatorio")]
        [Display(Name = "País")]
        public virtual Usuario? Usuario { get; set; }

        public ICollection<PedidoProducto> PedidoProductos { get; set; }
    }
}
