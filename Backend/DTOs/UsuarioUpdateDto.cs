namespace Backend.DTOs
{
    public class UsuarioUpdateDto
    {
        public int UsuarioID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public int TipoUsuarioId { get; set; }
    }
}
