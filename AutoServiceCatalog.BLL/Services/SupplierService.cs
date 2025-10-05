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
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SupplierService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierDto>> GetAllAsync()
        {
            var suppliers = await _unitOfWork.Suppliers.GetAllAsync();
            return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        }

        public async Task<SupplierDto> GetByIdAsync(int id)
        {
            var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);

            if (supplier == null)
                throw new Exception("Постачальника не знайдено");

            return _mapper.Map<SupplierDto>(supplier);
        }

        public async Task<SupplierDto> CreateAsync(SupplierCreateDto dto)
        {
            var entity = _mapper.Map<Supplier>(dto);
            await _unitOfWork.Suppliers.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SupplierDto>(entity);
        }

        public async Task UpdateAsync(int id, SupplierCreateDto dto)
        {
            var existing = await _unitOfWork.Suppliers.GetByIdAsync(id);

            if (existing == null)
                throw new Exception("Постачальника не знайдено");

            existing.Name = dto.Name;
            existing.Phone = dto.Phone;

            _unitOfWork.Suppliers.UpdateAsync(existing);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _unitOfWork.Suppliers.GetByIdAsync(id);

            if (existing == null)
                throw new Exception("Постачальника не знайдено");

            _unitOfWork.Suppliers.DeleteAsync(existing);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
