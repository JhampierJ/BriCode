using Bricons.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

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
        public DateTime? Fecha { get; set; }

        public virtual Producto? Producto { get; set; }

        public virtual Usuario? Usuario { get; set; }
    }
}
