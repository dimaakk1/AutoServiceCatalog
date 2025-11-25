using AutoServiceCatalog.DAL.Db;
using AutoServiceCatalog.DAL.Entities;
using AutoServiceCatalog.DAL.QueryParametrs;
using AutoServiceCatalog.DAL.Repositories.Intarfaces;
using AutoServiceCatalog.DAL.Specefication;
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

        public async Task<PagedResult<Part>> GetPartsAsync(PartQueryParameters parameters)
        {
            var spec = new PartSpecification(parameters);

            var query = SpecificationEvaluator.GetQuery(_context.Parts.AsQueryable(), spec);

            var totalCount = await _context.Parts
                .Where(spec.Criteria)
                .CountAsync();

            var items = await query.ToListAsync();

            return new PagedResult<Part>(items, totalCount, parameters.PageSize);
        }

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

        public async Task<List<Part>> SearchByNameAsync(string keyword)
        {
            return await _context.Parts
                .Where(p => p.Name.Contains(keyword))
                .ToListAsync();
        }
    }
}
