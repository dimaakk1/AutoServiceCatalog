using AutoServiceCatalog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceCatalog.DAL.Repositories.Intarfaces
{
    public interface IPartSupplierRepository : IGenericRepository<PartSupplier>
    {
        Task<List<Supplier>> GetSuppliersByPartIdAsync(int partId);
        Task<List<Part>> GetPartsBySupplierIdAsync(int supplierId);
    }
}
