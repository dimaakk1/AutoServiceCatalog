using AutoServiceCatalog.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceCatalog.BLL.Services.Interfaces
{
    public interface IPartService
    {
        Task<IEnumerable<PartDto>> GetAllAsync();
        Task<PartDto> GetByIdAsync(int id);
        Task<PartDto> CreateAsync(PartCreateDto dto);
        Task UpdateAsync(int id, PartCreateDto dto);
        Task DeleteAsync(int id);
    }
}
