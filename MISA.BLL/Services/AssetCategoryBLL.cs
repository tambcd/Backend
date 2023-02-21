using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.BLL.Interface;
using MISA.Common.Entity;
using MISA.DL.Interface;

namespace MISA.BLL.Services
{
    public class AssetCategoryBLL : BaseBLL<fixed_asset_category>,IAssetCategoryBLL
    {
        public AssetCategoryBLL(IRepository<fixed_asset_category> _repository) : base(_repository)
        {
        }
    }
}
