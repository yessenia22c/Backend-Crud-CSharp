using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class UsuarioService : IUsuarioService
    {
        private TiendaContext _context;

        public UsuarioService(TiendaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UsuarioDto>> Get() =>
            await _context.Usuarios.Select(u => new UsuarioDto
            {
                UsuarioID = u.UsuarioId,
                UserName = u.UserName,
                Email = u.Email,
                TipoUsuarioId = u.TipoUsuarioID
            }).ToListAsync();
        
        public async Task<UsuarioDto> GetById(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario != null)
            {
                var usuarioDto = new UsuarioDto
                {
                    UsuarioID = usuario.UsuarioId,
                    UserName = usuario.UserName,
                    Email = usuario.Email,
                    TipoUsuarioId = usuario.TipoUsuarioID
                };
                return usuarioDto;
            }
            
            return null;
        }
        public async Task<UsuarioDto> Add(UsuarioInsertDto usuarioInsertDto)
        {
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

            return usuarioDto;

        }     
        public async Task<UsuarioDto> Update(int id, UsuarioUpdateDto usuarioUpdateDto)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
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

                return usuarioDto;
            }
            return null;
        }
        public async Task<UsuarioDto> Delete(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario != null)
            {
                var usuarioDto = new UsuarioDto
                {
                    UsuarioID = usuario.UsuarioId,
                    UserName = usuario.UserName,
                    Email = usuario.Email,
                    TipoUsuarioId = usuario.TipoUsuarioID
                };
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                return usuarioDto;
            }


            return null;
        }
    }
}