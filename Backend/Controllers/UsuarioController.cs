using Backend.DTOs;
using Backend.Models;
using Backend.Services;
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
        // private TiendaContext _context;
        private IValidator<UsuarioInsertDto> _usuarioInsertValidator;
        private IValidator<UsuarioUpdateDto> _usuarioUpdateValidator;

        private ICommonService<UsuarioDto, UsuarioInsertDto, UsuarioUpdateDto> _usuarioService;

        public UsuarioController(
            // TiendaContext context,
            IValidator<UsuarioInsertDto> usuarioInsertValidator,
            IValidator<UsuarioUpdateDto> usuarioUpdateValidator,
            [FromKeyedServices("usuarioService")] ICommonService<UsuarioDto, UsuarioInsertDto, UsuarioUpdateDto> usuarioService
            )
        {
            // _context = context;
            _usuarioInsertValidator = usuarioInsertValidator;
            _usuarioUpdateValidator = usuarioUpdateValidator;
            _usuarioService = usuarioService;
        }
        [HttpGet]
        public async Task<IEnumerable<UsuarioDto>> Get() =>
            await _usuarioService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetById(int id)
        {
            var usuarioDto = await _usuarioService.GetById(id);

            return usuarioDto == null? NotFound() : Ok(usuarioDto);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> Add(UsuarioInsertDto usuarioInsertDto)
        {
            var validationResult = await _usuarioInsertValidator.ValidateAsync(usuarioInsertDto);
            if (!validationResult.IsValid) 
            {
                return BadRequest(validationResult.Errors);
            }
            
            var usuarioDto = await _usuarioService.Add(usuarioInsertDto);

            return CreatedAtAction(nameof(GetById), new { id = usuarioDto.UsuarioID }, usuarioDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDto>> Update(int id, UsuarioUpdateDto usuarioUpdateDto)
        {
            var validationResult = await _usuarioUpdateValidator.ValidateAsync(usuarioUpdateDto);
            if(!validationResult.IsValid){
                return BadRequest(validationResult.Errors);
            }
            
            var usuarioDto = await _usuarioService.Update(id, usuarioUpdateDto);
           
            return usuarioDto == null? NotFound() : Ok(usuarioDto);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioDto>> Delete(int id)
        {
            var usuarioDto = await _usuarioService.Delete(id);
            return usuarioDto == null ? NotFound() : Ok(usuarioDto);
        }
    }
}
