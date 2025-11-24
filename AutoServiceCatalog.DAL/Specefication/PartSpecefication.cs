using AutoServiceCatalog.DAL.Entities;
using AutoServiceCatalog.DAL.QueryParametrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceCatalog.DAL.Specefication
{
    public class PartSpecification : Specification<Part>
    {
        public PartSpecification(PartQueryParameters parameters)
        {
            Criteria = p =>
                (string.IsNullOrEmpty(parameters.Search) ||
                    p.Name.Contains(parameters.Search)) &&
                (!parameters.CategoryId.HasValue ||
                    p.CategoryId == parameters.CategoryId.Value);

            // Include залежностей
            Includes.Add(p => p.Category);
            Includes.Add(p => p.PartDetail);
            Includes.Add(p => p.PartSuppliers);

            // Сортування
            if (!string.IsNullOrEmpty(parameters.SortBy))
            {
                OrderBy = parameters.SortBy switch
                {
                    "name" => q => q.OrderBy(p => p.Name),
                    "price" => q => q.OrderBy(p => p.Price),
                    _ => OrderBy
                };

                if (parameters.Desc)
                {
                    OrderByDescending = parameters.SortBy switch
                    {
                        "name" => q => q.OrderByDescending(p => p.Name),
                        "price" => q => q.OrderByDescending(p => p.Price),
                        _ => OrderByDescending
                    };
                }
            }

            Skip = (parameters.PageNumber - 1) * parameters.PageSize;
            Take = parameters.PageSize;
        }
    }
}
