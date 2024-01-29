using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;

namespace Backend.Services
{
    public interface ICommonService<T, TI, TU>
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(TI usuarioInsertDto);
        Task<T> Update(int id, TU usuarioUpdateDto);
        Task<T> Delete(int id);
    }
}