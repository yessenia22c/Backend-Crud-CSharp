namespace Backend.DTOs
{
    public class UsuarioDto
    {
        public int UsuarioID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public int TipoUsuarioId { get; set; }
    }
}
