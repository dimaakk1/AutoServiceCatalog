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
    public class PartDetailService : IPartDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PartDetailService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<PartDetailDto>> GetAllAsync()
        {
            var entities = await _unitOfWork.PartDetail.GetAllAsync();
            return _mapper.Map<List<PartDetailDto>>(entities);
        }

        public async Task<PartDetailDto?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.PartDetail.GetByIdAsync(id);
            return _mapper.Map<PartDetailDto>(entity);
        }

        public async Task<PartDetailDto> CreateAsync(PartDetailCreateDto dto)
        {
            var part = await _unitOfWork.Parts.GetByIdAsync(dto.PartId);
            if (part == null)
                throw new Exception("Part not found");

            var entity = _mapper.Map<PartDetail>(dto);
            entity.PartId = dto.PartId; 

            await _unitOfWork.PartDetail.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<PartDetailDto>(entity); ;
        }

        public async Task UpdateAsync(int id, PartDetailCreateDto dto)
        {
            var existing = await _unitOfWork.PartDetail.GetByIdAsync(id);
            if (existing == null)
                throw new Exception("PartDetail not found");

            existing.Manufacturer = dto.Manufacturer;
            existing.Warranty = dto.Warranty;

            _unitOfWork.PartDetail.UpdateAsync(existing);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _unitOfWork.PartDetail.GetByIdAsync(id);
            if (existing == null)
                throw new Exception("PartDetail not found");

            _unitOfWork.PartDetail.DeleteAsync(existing);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<List<PartDetailDto>> GetByManufacturerAsync(string manufacturer)
        {
            var details = await _unitOfWork.PartDetail.GetByManufacturerAsync(manufacturer);
            return _mapper.Map<List<PartDetailDto>>(details);
        }
    }
}
