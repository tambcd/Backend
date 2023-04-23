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
        /// @created by : tvTam
        /// @create day : 19/3/2023
        /// </summary>
        /// <param name="pageNumber">số trang</param>
        /// <param name="pageSize">số bảng ghi trên trang </param>
        /// <param name="txtSearch"> từ khóa tìm kiếm </param>
        /// <param name="DepartmentId"> mã phòng ban tìm kiếm </param>
        /// <param name="AssetCategoryId"> Mã loại tài sản tìm kiếm </param>
        /// <returns></returns>
        /// created : TVTam(MF1270) - 22/02/2022
        public PagingRequest<fixed_asset> GetFilter(int pageNumber, int pageSize, string? txtSearch,Guid? DepartmentId,Guid? AssetCategoryId);
        
        /// <summary>
        /// danh sách tài sản phân trang chưa được chọn 
        /// </summary>
        /// <param name="codes">chuỗi các code cảu tài sản đã được chọn </param>
        /// <param name="pageNumber">số trang hiện tại </param>
        /// <param name="pageSize">số bản ghi trên trang </param>
        /// <param name="txtSearch">từ khóa tìm kiếm</param>
        /// <returns>danh sách tài sản thỏa mãn </returns>
   
        public IEnumerable<fixed_asset> Getpage(string? txtSearch,Guid? DepartmentId,Guid? AssetCategoryId);
        /// <summary>
        /// nhập khâủ dữ liệu 
        /// @created by : tvTam
        /// @create day : 19/03/2023
        /// </summary>
        /// <param name="assets">danh sách tài sản </param>
        /// <returns>số bản ghi thành công || thất bại : 0</returns>
        public int Import(List<fixed_asset> assets);

        /// <summary>
        /// Lấy dánh sánh sản phẩm theo chứng từ 
        /// </summary>
        /// <param name="idLicense">id chứng từ </param>
        /// <returns></returns>
        public IEnumerable<fixed_asset> GetByLicense(Guid idLicense);

        /// <summary>
        /// Cập nhập nguyền giá cho tài sản 
        /// </summary>
        /// <param name="idAsset">id tài sản </param>
        /// <param name="idLicense">id chứng từ </param>
        /// <param name="cost">nguyên giá </param>
        /// <param name="new_cost">nfuoonf nguyên giá</param>
        /// <returns></returns>
        public int UpdateCost(Guid idAsset, Guid idLicense,double cost, List<string> new_cost);

    }
}
