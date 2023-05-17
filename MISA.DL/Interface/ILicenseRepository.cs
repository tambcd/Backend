using MISA.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.DL.Interface
{
    public interface ILicenseRepository: IRepository<license>
    {
        /// <summary>
        /// thêm chứng từ 
        /// </summary>
        /// <param name="license">chứng từ </param>
        /// <param name="ids">danh sách id </param>
        /// <returns>thất bại 0 || thành công số bản ghi được thêm </returns>
        public EntityReturn Insertlicense(license license, List<Guid> ids);
        /// <summary>
        ///  Sửa chứng từ 
        /// </summary>
        /// <param name="updateLicense">thồng tin chứng từ</param>
        /// <returns>đối tượng chưá trang thái thành công || lỗi </returns>

        public EntityReturn Updatelicense(EntityUpdateLicense updateLicense);

        /// <summary>
        /// Cập nhập trạng thái khi xóa chứng từ 
        /// </summary>
        /// <param name="ids">danh sách hóa chính tài sản xóa </param>
        /// <returns>số bản ghi xóa </returns>
        public int UpdateActiveAsset(List<Guid> ids);

    }
}
