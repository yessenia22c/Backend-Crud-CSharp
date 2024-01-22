namespace Backend.DTOs
{
    public class UsuarioInsertDto
    {
        
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public int TipoUsuarioID { get; set; }
    }
}
