using Backend.DTOs;
using Backend.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private TiendaContext _context;
        private IValidator<UsuarioInsertDto> _usuarioInsertValidator;
        private IValidator<UsuarioUpdateDto> _usuarioUpdateValidator;

        public UsuarioController(TiendaContext context,
            IValidator<UsuarioInsertDto> usuarioInsertValidator,
            IValidator<UsuarioUpdateDto> usuarioUpdateValidator
            )
        {
            _context = context;
            _usuarioInsertValidator = usuarioInsertValidator;
            _usuarioUpdateValidator = usuarioUpdateValidator;
        }
        [HttpGet]
        public async Task<IEnumerable<UsuarioDto>> Get() =>
            await _context.Usuarios.Select(u => new UsuarioDto
            {
                UsuarioID = u.UsuarioId,
                UserName = u.UserName,
                Email = u.Email,
                TipoUsuarioId = u.TipoUsuarioID
            }).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetById(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }
            var usuarioDto = new UsuarioDto {
                UsuarioID = usuario.UsuarioId,
                UserName = usuario.UserName,
                Email = usuario.Email,
                TipoUsuarioId = usuario.TipoUsuarioID
            };
            return Ok(usuarioDto);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> Add(UsuarioInsertDto usuarioInsertDto)
        {
            var validationResult = await _usuarioInsertValidator.ValidateAsync(usuarioInsertDto);
            if (!validationResult.IsValid) 
            {
                return BadRequest(validationResult.Errors);
            }
            var usuario = new Usuario()
            {
                UserName = usuarioInsertDto.UserName,
                Password = usuarioInsertDto.Password,
                Email = usuarioInsertDto.Email,
                TipoUsuarioID = usuarioInsertDto.TipoUsuarioID

            };
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            var usuarioDto = new UsuarioDto
            {
                UsuarioID = usuario.UsuarioId,
                UserName = usuario.UserName,
                Email = usuario.Email,
                TipoUsuarioId = usuario.TipoUsuarioID
            };

            return CreatedAtAction(nameof(GetById), new { id = usuario.UsuarioId }, usuarioDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDto>> Update(int id, UsuarioUpdateDto usuarioUpdateDto)
        {
            var validationResult = await _usuarioUpdateValidator.ValidateAsync(usuarioUpdateDto);
            if(!validationResult.IsValid){
                return BadRequest(validationResult.Errors);
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if(usuario == null)
            {
                return NotFound();

            }
            usuario.UserName = usuarioUpdateDto.UserName;
            usuario.Password = usuarioUpdateDto.Password;
            usuario.Email = usuarioUpdateDto.Email;
            usuario.TipoUsuarioID = usuarioUpdateDto.TipoUsuarioId;

            await _context.SaveChangesAsync();
            var usuarioDto = new UsuarioDto
            {
                UsuarioID = usuario.UsuarioId,
                UserName = usuario.UserName,
                Email = usuario.Email,
                TipoUsuarioId = usuario.TipoUsuarioID
            };

            return Ok(usuarioDto);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
