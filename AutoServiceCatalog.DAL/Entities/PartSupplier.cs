using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceCatalog.DAL.Entities
{
    public class PartSupplier
    {
        public int PartId { get; set; }
        public Part Part { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
