using AutoMapper;
using AutoServiceCatalog.BLL.DTO;
using AutoServiceCatalog.BLL.Services.Interfaces;
using AutoServiceCatalog.DAL.Entities;
using AutoServiceCatalog.DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceCatalog.BLL.Services
{
    public class PartSupplierService : IPartSupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PartSupplierService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<PartSupplierDto>> GetAllAsync()
        {
            var entities = await _unitOfWork.PartSupplier.GetAllAsync();
            return _mapper.Map<List<PartSupplierDto>>(entities);
        }

        public async Task<PartSupplierDto?> GetByIdsAsync(int partId, int supplierId)
        {
            var all = await _unitOfWork.PartSupplier.GetAllAsync();
            var entity = all.FirstOrDefault(ps => ps.PartId == partId && ps.SupplierId == supplierId);

            return entity != null ? _mapper.Map<PartSupplierDto>(entity) : null;
        }

        public async Task<PartSupplierDto> CreateAsync(PartSupplierDto dto)
        {
            var entity = _mapper.Map<PartSupplier>(dto);
            await _unitOfWork.PartSupplier.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<PartSupplierDto>(entity);
        }

        public async Task DeleteAsync(int partId, int supplierId)
        {
            var all = await _unitOfWork.PartSupplier.GetAllAsync();
            var entity = all.FirstOrDefault(ps => ps.PartId == partId && ps.SupplierId == supplierId);

            if (entity == null)
                throw new Exception("PartSupplier link not found");

            _unitOfWork.PartSupplier.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
