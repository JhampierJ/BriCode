using System.ComponentModel.DataAnnotations;

namespace Bricons.Models
{
    public class Cotizacion
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El ID Usuario es obligatorio")]
        [Display(Name = "ID Usuario")]
        public int? UsuarioId { get; set; }

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaVencimineto { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Sucursal")]
        public string Sucursal { get; set; }

        [Required(ErrorMessage = "El pais es obligatorio")]
        [Display(Name = "País")]
        public string Pais { get; set; }
        [Required(ErrorMessage = "La ciudad es obligatorio")]
        [Display(Name = "Ciudad")]
        public string Ciudad { get; set; }
        [Required(ErrorMessage = "La dirección es obligatoria")]
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }
        public bool confirmacion { get; set; } = false;
        public virtual Usuario? Usuario { get; set; }

        public ICollection<CotizacionProducto> CotizacionProductos { get; set; }
    }
}
