using InventarioVentas.Models;

public class VentaProducto
{
    public int Id { get; set; }
    public int VentaId { get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }

    public Venta Venta { get; set; }
    public Producto Producto { get; set; }
}