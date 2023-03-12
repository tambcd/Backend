using MISA.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.BLL.Interface
{
    public interface IAssetBLL:IBaseBLL<fixed_asset>
    {

        /// <summary>
        /// Xuất khẩu tất cả nhân viên ra excel
        /// </summary>
        /// <returns>URL tải excle xuống </returns>
        public Stream ExportAssets();
        
        

    }
}
