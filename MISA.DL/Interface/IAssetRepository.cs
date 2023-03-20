using MISA.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.DL.Interface
{
    public interface IAssetRepository : IRepository<fixed_asset>
    {
        /// <summary>
        /// Lọc danh sách đối tượng
        /// </summary>
        /// <param name="pageNumber">số trang</param>
        /// <param name="pageSize">số bảng ghi trên trang </param>
        /// <param name="txtSearch"> từ khóa tìm kiếm </param>
        /// <param name="DepartmentId"> mã phòng ban tìm kiếm </param>
        /// <param name="AssetCategoryId"> Mã loại tài sản tìm kiếm </param>
        /// <returns></returns>
        /// created : TVTam(MF1270) - 22/02/2022
        public PagingRequest GetFilter(int pageNumber, int pageSize, string? txtSearch,Guid? DepartmentId,Guid? AssetCategoryId);

    }
}
