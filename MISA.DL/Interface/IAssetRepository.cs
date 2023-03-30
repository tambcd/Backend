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

        /// <summary>
        /// lấy thông tin dành cho phân trang 
        /// </summary>
        /// <param name="txtSearch">từ khóa tìm kiếm</param>
        /// <param name="DepartmentId">mã phòng ban tìm kiếm</param>
        /// <param name="AssetCategoryId">Mã loại tài sản tìm kiếm</param>
        /// <returns></returns>
        public IEnumerable<fixed_asset> Getpage(string? txtSearch,Guid? DepartmentId,Guid? AssetCategoryId);
        /// <summary>
        /// nhập khâủ dữ liệu 
        /// </summary>
        /// <param name="assets">danh sách tài sản </param>
        /// <returns>số bản ghi thành công || thất bại : 0</returns>
        public int Import(List<fixed_asset> assets);

    }
}
