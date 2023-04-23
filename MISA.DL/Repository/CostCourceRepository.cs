using Microsoft.Extensions.Configuration;
using MISA.Common.Entity;
using MISA.DL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.DL.Repository
{
    public class CostCourceRepository : BaseRepository<cost_source>, ICostCourceRepository
    {
        public CostCourceRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
