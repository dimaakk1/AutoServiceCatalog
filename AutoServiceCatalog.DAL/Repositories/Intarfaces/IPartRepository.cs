using AutoServiceCatalog.DAL.Entities;
using AutoServiceCatalog.DAL.QueryParametrs;
using AutoServiceCatalog.DAL.Specefication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceCatalog.DAL.Repositories.Intarfaces
{
    public interface IPartRepository : IGenericRepository<Part>
    {
        Task<List<Part>> GetPartsAbovePriceAsync(decimal price);
        Task<List<Part>> GetPartsBelowPriceAsync(decimal price);
        Task<List<Part>> SearchByNameAsync(string keyword);

        Task<PagedResult<Part>> GetPartsAsync(PartQueryParameters parameters);
    }
}
