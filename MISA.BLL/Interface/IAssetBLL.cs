using Microsoft.AspNetCore.Http;
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
        /// Nhập khẩu danh sách tài sản 
        /// </summary>
        /// <param name="file">file excel đầu vào</param>
        /// <returns> danh sách tài sản hợp lệ</returns>
        public int ImportAssets(IFormFile file);

      
        /// <summary>
        /// Xuất khẩu tất cả nhân viên ra excel
        /// </summary>
        /// <param name="txtSearch">từ khóa tìm kiếm </param>
        /// <param name="DepartmentId">khóa chính phòng ban</param>
        /// <param name="AssetCategoryId">khóa chính loại tài sản</param>
        /// <returns>URL tải excle xuốn</returns>
        public Stream ExportAssets(string? txtSearch, Guid? DepartmentId, Guid? AssetCategoryId);
        
        

    }
}
