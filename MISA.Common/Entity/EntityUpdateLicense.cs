using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.Entity
{
    public class EntityUpdateLicense
    {
        public license license { get; set; }
        public List<Guid>? guidsDelete { get; set; } = new List<Guid>(); 
        public List<Guid>? guidsUpdate { get; set; } = new List<Guid>();
    }
}
