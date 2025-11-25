using AutoServiceCatalog.DAL.Repositories.Intarfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceCatalog.DAL.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IPartRepository Parts { get; }
        ICategoryRepository Categories { get; }
        ISupplierRepository Suppliers { get; }
        IPartDetailRepository PartDetail { get; }
        IPartSupplierRepository PartSupplier { get; }

        Task<int> SaveChangesAsync();
    }
}
