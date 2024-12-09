namespace InventarioVentas.Dto
{
    public class ClienteDto
    {
        public int Id { get; set; } // Identificador único del cliente
        public string Nombre { get; set; } // Nombre del cliente
        public string CorreoElectronico { get; set; } // Correo electrónico del cliente
        public string Telefono { get; set; } // Número de teléfono del cliente
        public string Direccion { get; set; } // Dirección del cliente
        public string Contraseña { get; set; }
        
    }
}
