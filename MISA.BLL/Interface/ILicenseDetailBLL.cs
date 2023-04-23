using MISA.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.BLL.Interface
{
    public interface ILicenseDetailBLL: IBaseBLL<license_detail>
    {
        /// <summary>
        /// thêm tài sản theo chứng từ 
        /// </summary>
        /// <param name="id">id chứng từ </param>
        /// <param name="ids">danh sách id tài sản  </param>
        /// <returns>thất bại 0 || thành công =  số bản ghi </returns>
        public int insertLicenseDetail(Guid idLicense, List<Guid> idsAsset);
    }
}
