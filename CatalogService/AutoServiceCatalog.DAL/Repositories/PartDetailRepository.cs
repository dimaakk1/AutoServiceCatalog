using AutoServiceCatalog.DAL.Db;
using AutoServiceCatalog.DAL.Entities;
using AutoServiceCatalog.DAL.Repositories.Intarfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceCatalog.DAL.Repositories
{
    public class PartDetailRepository : GenericRepository<PartDetail>, IPartDetailRepository
    {
        public PartDetailRepository(CarServiceContext context) : base(context) { }
        public async Task<List<PartDetail>> GetByManufacturerAsync(string manufacturer)
        {
            return await _context.PartDetails
                .Where(pd => pd.Manufacturer.ToLower().Contains(manufacturer.ToLower()))
                .Include(pd => pd.Part)
                .ToListAsync();
        }
    }
}
