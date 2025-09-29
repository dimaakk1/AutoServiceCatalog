using AutoServiceCatalog.DAL.Db;
using AutoServiceCatalog.DAL.Entities;
using AutoServiceCatalog.DAL.Repositories.Intarfaces;
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
    }
}
