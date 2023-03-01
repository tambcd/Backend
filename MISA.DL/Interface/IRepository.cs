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
        int deleteMany(List<Guid> ids);

    }
}
