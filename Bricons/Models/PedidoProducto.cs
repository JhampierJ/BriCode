﻿namespace Bricons.Models
{
    public class PedidoProducto
    {
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}