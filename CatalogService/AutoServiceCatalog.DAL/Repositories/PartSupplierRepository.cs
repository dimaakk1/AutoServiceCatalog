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
    public class PartSupplierRepository : GenericRepository<PartSupplier>, IPartSupplierRepository
    {
        public PartSupplierRepository(CarServiceContext context) : base(context) { }
        public async Task<List<Supplier>> GetSuppliersByPartIdAsync(int partId)
        {
            return await _context.PartSupplier
                .Where(ps => ps.PartId == partId)
                .Select(ps => ps.Supplier)
                .ToListAsync();
        }
        public async Task<List<Part>> GetPartsBySupplierIdAsync(int supplierId)
        {
            return await _context.PartSupplier
                .Where(ps => ps.SupplierId == supplierId)
                .Select(ps => ps.Part)
                .ToListAsync();
        }
    }
}
