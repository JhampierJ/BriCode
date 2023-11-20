using Bricons.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bricons.Models
{
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }
        public int CotizacionId { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaEntrega { get; set; }
        public string Estado {  get; set; }
        public Cotizacion Cotizacion { get; set; }
    }
}
