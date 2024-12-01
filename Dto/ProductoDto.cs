namespace InventarioVentas.Dtos
    {
        public class ProductoDto
        {
            public int Id { get; set; } // Identificador único del producto
            public string Nombre { get; set; } // Nombre del producto
            public decimal Precio { get; set; } // Precio del producto
            public int Stock { get; set; } // Cantidad en inventario
        }
    }


