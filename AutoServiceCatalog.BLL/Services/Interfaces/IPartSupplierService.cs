using AutoServiceCatalog.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceCatalog.BLL.Services.Interfaces
{
    public interface IPartSupplierService
    {
        Task<List<PartSupplierDto>> GetAllAsync();
        Task<PartSupplierDto?> GetByIdsAsync(int partId, int supplierId);
        Task<PartSupplierDto> CreateAsync(PartSupplierDto dto);
        Task DeleteAsync(int partId, int supplierId);
    }
}
