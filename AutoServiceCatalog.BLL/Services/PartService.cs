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
    public class PartService : IPartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PartDto>> GetAllAsync()
        {
            var parts = await _unitOfWork.Parts.GetAllAsync();
            return _mapper.Map<IEnumerable<PartDto>>(parts);
        }

        public async Task<PartDto> GetByIdAsync(int id)
        {
            var part = await _unitOfWork.Parts.GetByIdAsync(id);
            if (part == null)
                throw new Exception("Запчастину не знайдено");

            return _mapper.Map<PartDto>(part);
        }

        public async Task<PartDto> CreateAsync(PartCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Назва запчастини не може бути порожньою!");

            if (dto.Price <= 0)
                throw new ArgumentException("Ціна повинна бути більшою за 0!");

            var part = _mapper.Map<Part>(dto);

            await _unitOfWork.Parts.AddAsync(part);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<PartDto>(part);
        }

        public async Task UpdateAsync(int id, PartCreateDto dto)
        {
            var existing = await _unitOfWork.Parts.GetByIdAsync(id);
            if (existing == null)
                throw new Exception("Запчастину не знайдено");

            existing.Name = dto.Name;
            existing.Price = dto.Price;
            existing.CategoryId = dto.CategoryId;

            _unitOfWork.Parts.UpdateAsync(existing);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _unitOfWork.Parts.GetByIdAsync(id);
            if (existing == null)
                throw new Exception("Запчастину не знайдено");

            _unitOfWork.Parts.DeleteAsync(existing);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
