using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string  UserName { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }

        public int TipoUsuarioID { get; set; }

        [ForeignKey("TipoUsuarioID")]
        public virtual TipoUsuario TipoUsuario { get; set; }
    }
}
