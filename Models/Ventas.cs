namespace InventarioVentas.Models
{
    public class Venta
    {
        public int Id { get; set; } // Identificador único de la venta
        public DateTime Fecha { get; set; } // Fecha de la venta
        public decimal Total { get; set; } // Total de la venta

        // Clave foránea para Cliente
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } // Relación con el cliente
    }
}