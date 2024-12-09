namespace InventarioVentas.Dto
{
    public class VentaDto
    {
        public int Id { get; set; } // Identificador único de la venta
        public DateTime Fecha { get; set; } // Fecha de la venta
        public decimal Total { get; set; } // Total de la venta

        // Clave foránea para Cliente
        public int ClienteId { get; set; }
        public List<VentaProductoDto> VentaProductos { get; set; } = new List<VentaProductoDto>();


    }
    public class VentaProductoDto
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}
