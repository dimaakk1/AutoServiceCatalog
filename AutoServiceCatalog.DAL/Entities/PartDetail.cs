using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceCatalog.DAL.Entities
{
    public class PartDetail
    {
        public int PartDetailId { get; set; }
        public string Manufacturer { get; set; } = null!;
        public string Warranty { get; set; }

        public int PartId { get; set; }
        public Part Part { get; set; }
    }
}
