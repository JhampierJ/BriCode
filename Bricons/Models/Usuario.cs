namespace Bricons.Models
{
    public class Usuario
    {
        public int Id { get; set; } 
        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public string Dni { get; set; }

        public string NombreUsuario { get; set; }

        public virtual ICollection<Pedido>? Pedidos { get; set; } = new List<Pedido>();

        public virtual ICollection<Programacion>? Programaciones { get; set; } = new List<Programacion>();
    }
}
