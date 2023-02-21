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
    public class AssetCategoryRepository : BaseRepository<fixed_asset_category>, IAssetCategoryRepository
    {
        public AssetCategoryRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
