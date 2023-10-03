using Bricons.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bricons.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }
    }
}
