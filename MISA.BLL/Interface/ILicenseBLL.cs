using MISA.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.BLL.Interface
{
    public interface ILicenseBLL: IBaseBLL<license>
    {
        /// <summary>
        /// thêm chứng từ 
        ///  CreatedBy : TVTam(20/02/2023)
        /// </summary>
        /// <param name="license">chứng từ </param>
        /// <param name="idsAsset">danh sách id tài sản</param>
        /// <returns>đối tượng chưá trang thái thành công || lỗi </returns>

        public EntityReturn InsertLicense(license license, List<Guid> idsAsset);
        /// <summary>
        /// Sửa chứng từ và chi tiết 
        ///  CreatedBy : TVTam(20/02/2023)
        /// </summary>
        /// <param name="updateLicense"> chứng từ và list danh sách id tài sản thêm vào xóa</param>
        /// <returns>đối tượng chưá trang thái thành công || lỗi </returns>

        public EntityReturn UpdateLicense(EntityUpdateLicense updateLicense);
    }
}
