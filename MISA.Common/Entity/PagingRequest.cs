using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.Entity
{
    public class PagingRequest
    {
        public int TotalPage { get; set; }

        public int TotalRecord { get; set; }
        public int CurrentPage { get; set; }

        public int CurrentPageRecords { get; set; }

        public List<fixed_asset> Data { get; set; } = new List<fixed_asset> { };
    }
}
