namespace InventarioVentas.Dto
{
    public class RegistroDto
    {
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; } // Cliente, Vendedor o Administrador
    }
}
