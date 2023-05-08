using MISA.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.DL.Interface
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Lấy tất cả bản ghi 
        ///  @ createBy TVTam(MF1270) - 20/02/2023
        ///  
        /// </summary>
        /// <returns>Danh sánh đối tương </returns>
        public IEnumerable<T> GetAll();

        /// <summary>
        /// Lấy bản ghi theo Id
        /// createBy TVTam(MF1270) - 20/02/2023
        /// </summary>
        /// <param name="id">khóa chính nhân viên cần lấy</param>
        /// <returns> bản ghi có Id cần tìm</returns>
        public T GetById(Guid id);
        /// <summary>
        /// Lấy bản ghi theo mã 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public T GetByCode(string code);


        /// <summary>
        /// Xóa bản ghi
        /// createBy TVTam(MF1270) - 20/02/2023
        /// </summary>
        /// <param name="id">khóa chính đối tượng cần xóa</param>
        /// <returns>200 : thành công </returns>
        /// 
        public int Delete(Guid id);

        /// <summary>
        /// Thêm bản ghi
        /// createBy TVTam(MF1270) - 20/02/2023
        /// </summary>
        /// <param name="entiy">Bản ghi cần thêm</param>
        /// <returns> 201 nếu đã thêm</returns>

        public int Insert(T entiy);

        /// <summary>
        /// Cập nhật bản ghi
        /// @@ createBy TVTam(MF1270) - 20/02/2023
        /// </summary>
        /// <param name="id">Khóa chính đối tượng cần sửa</param>
        /// <param name="entity">Thông tin cần sửa</param>
        /// <returns>200 nếu chưa update</returns>

        public int Update(T entity);
        /// <summary>
        /// Xóa nhiều
        /// </summary>
        /// <param name="ids">Danh sach khóa chính của các đối tượng cần xóa</param>
        /// <returns></returns>
        /// createBy TVTam(MF1270) - 28/02/2023
        int DeleteMany(List<Guid> ids);
        /// <summary>
        /// xóa nhiều theo mã 
        /// </summary>
        /// <param name="codes">danh sách mã </param>
        /// <returns>o|| số bản ghi được xóa </returns>
        int DeleteManyByCode(List<string> codes);

        /// <summary>
        /// kiểm tra mã trùng 

        /// </summary>
        /// <param name="code">mã đối tượng </param>
        /// <returns>
        /// @@ false : không trùng 
        /// @@ true : trùng 
        /// </returns>
        public bool IsSameCode(string code, Guid? id);

        /// <summary>
        /// Lấy ra mã mới nhất 
        /// </summary>
        /// <returns></returns>
        public string GetCodeNewfirst();

        /// <summary>
        /// lấy giá trị mã mới 
        /// </summary>
        /// <returns></returns>
        public string GetAutoCode(string txt);

        /// <summary>
        /// lấy bản ghi hoạt động và chưa được chọn 
        /// </summary>
        /// <param name="ids">danh sách id bản khi không hiển thị</param>
        /// <returns>danh sách bản ghi </returns>
        public IEnumerable<T> GetRecordActive(List<Guid> ids);

        /// <summary>
        /// danh sách tài sản phân trang chưa được chọn 
        /// </summary>
        /// <param name="codes">chuỗi các code cảu tài sản đã được chọn </param>
        /// <param name="pageNumber">số trang hiện tại </param>
        /// <param name="pageSize">số bản ghi trên trang </param>
        /// <param name="txtSearch">từ khóa tìm kiếm</param>
        /// <returns>danh sách tài sản thỏa mãn </returns>
        public PagingRequest<T> GetSreachBase(string codes, int pageNumber, int pageSize, string? txtSearch, Guid? idLicense);

        /// <summary>
        /// danh sách tài sản phân trang chưa được chọn 
        /// </summary>
        /// <param name="codes">chuỗi các code cảu tài sản đã được chọn </param>
        /// <param name="pageNumber">số trang hiện tại </param>
        /// <param name="pageSize">số bản ghi trên trang </param>
        /// <param name="txtSearch">từ khóa tìm kiếm</param>
        /// <returns>danh sách tài sản thỏa mãn </returns>
        public PagingRequest<T> GetSelectItem(string codes, int pageNumber, int pageSize, string? txtSearch);

        
    }
}
