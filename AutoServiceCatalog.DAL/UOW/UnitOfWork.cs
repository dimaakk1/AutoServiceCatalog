using AutoServiceCatalog.DAL.Db;
using AutoServiceCatalog.DAL.Repositories.Intarfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceCatalog.DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarServiceContext _context;

        public IPartRepository Parts { get; }
        public ICategoryRepository Categories { get; }
        public ISupplierRepository Suppliers { get; }
        public IPartDetailRepository PartDetail { get; }
        public IPartSupplierRepository PartSupplier { get; }

        public UnitOfWork(
            CarServiceContext context,
            IPartRepository partRepository,
            ICategoryRepository categoryRepository,
            ISupplierRepository supplierRepository,
            IPartSupplierRepository partSupplierRepository,
            IPartDetailRepository partDetailRepository)
        {
            _context = context;
            Parts = partRepository;
            Categories = categoryRepository;
            Suppliers = supplierRepository;
            PartDetail = partDetailRepository;
            PartSupplier = partSupplierRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
