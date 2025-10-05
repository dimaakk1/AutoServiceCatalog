using AutoServiceCatalog.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceCatalog.BLL.Services.Interfaces
{
    public interface IPartDetailService
    {
        Task<List<PartDetailDto>> GetAllAsync();
        Task<PartDetailDto?> GetByIdAsync(int id);
        Task<PartDetailDto> CreateAsync(PartDetailCreateDto dto);
        Task UpdateAsync(int id, PartDetailCreateDto dto);
        Task DeleteAsync(int id);
    }
}
