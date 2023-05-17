using MISA.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.BLL.Interface
{
    public interface IBaseBLL<T>
    {
        /// <summary>
        /// Thêm mới đối tương
        /// @  CreatedBy : TVTam(20/02/2023)
        /// </summary>
        /// <param name="entity">đối tượng thêm mới</param>
        /// <returns>đối tượng chưá trang thái thành công || lỗi </returns>

        public EntityReturn InsertSevices(T entity);

        /// <summary>
        /// Sửa đối tượng
        ///  @  CreatedBy : TVTam(20/02/2023)
        /// </summary>
        /// <param name="entity">đối tượng cần sửa</param>
        /// <returns>đối tượng chưá trang thái thành công || lỗi </returns>
        public EntityReturn UpdateSevices(T entity);


        /// <summary>
        /// Xóa đối tượng
        ///  @  CreatedBy : TVTam(20/02/2023)
        /// </summary>
        /// <param name="id">Khóa chính dôid tượng cần xóa</param>
        /// <returns>200 thành công</returns>
        public int DeleteSevices(Guid id);

        /// <summary>
        /// Validate đối tượng 
        /// CreatedBy : TVTam(20/02/2023)
        /// </summary>
        /// <param name="entity">đối tượng cần validate</param>
        /// <param name="type">
        /// insert : "insert"
        /// update : "update"
        /// </param>
        /// <returns>
        /// true : thỏa mán validate
        /// Fasle : không thỏa mãn validate     
        /// </returns>
        public bool Validate(T entity , int type);

        /// <summary>
        /// Sinh mã từ động 
        ///  CreatedBy : TVTam(20/02/2023)
        /// </summary>
        /// <param name="code">mã tài sản </param>
        /// <returns>chuỗi mã mới</returns>
        public string AutoCodeSevices();

        /// <summary>
        /// Xóa nhiểu đối tượng
        ///  CreatedBy : TVTam(20/02/2023)
        /// </summary>
        /// <param name="guids"></param>
        /// <returns>đối tượng chưá trang thái thành công || lỗi </returns>
        public EntityReturn DeleteManyService(List<Guid> guids);
    }
}
