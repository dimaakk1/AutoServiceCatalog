using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceCatalog.BLL.DTO
{
    public class PartDetailDto
    {
        public int PartDetailId { get; set; }
        public int PartId { get; set; }
        public string Manufacturer { get; set; }
        public string Warranty { get; set; }
    }
}
