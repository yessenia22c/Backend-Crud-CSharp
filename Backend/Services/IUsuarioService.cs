using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;

namespace Backend.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> Get();
        Task<UsuarioDto> GetById(int id);
        Task<UsuarioDto> Add(UsuarioInsertDto usuarioInsertDto);
        Task<UsuarioDto> Update(int id, UsuarioUpdateDto usuarioUpdateDto);
        Task<UsuarioDto> Delete(int id);
    }
}