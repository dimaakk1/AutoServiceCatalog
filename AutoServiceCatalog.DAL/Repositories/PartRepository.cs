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
    public class PartRepository : GenericRepository<Part>, IPartRepository
    {
        public PartRepository(CarServiceContext context) : base(context) { }

        public async Task<List<Part>> GetPartsAbovePriceAsync(decimal price)
        {
            return await _context.Parts
                .Where(p => p.Price > price)
                .ToListAsync();
        }

        public async Task<List<Part>> GetPartsBelowPriceAsync(decimal price)
        {
            return await _context.Parts
                .Where(p => p.Price < price)
                .ToListAsync();
        }
    }
}
