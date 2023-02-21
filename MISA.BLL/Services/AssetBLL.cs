using MISA.BLL.Interface;
using MISA.Common.Entity;
using MISA.DL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.BLL.Services
{
    public class AssetBLL : BaseBLL<fixed_asset>, IAssetBLL
    {
        public AssetBLL(IRepository<fixed_asset> _repository) : base(_repository)
        {
        }

        
    }
}
