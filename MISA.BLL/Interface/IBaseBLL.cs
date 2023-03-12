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
        /// <returns>201 thành công</returns>

        public int InsertSevices(T entity);

        /// <summary>
        /// Sửa đối tượng
        ///  @  CreatedBy : TVTam(20/02/2023)
        /// </summary>
        /// <param name="entity">đối tượng cần sửa</param>
        /// <returns>200 Thành công</returns>
        public int UpdateSevices(T entity);


        /// <summary>
        /// Xóa đối tượng
        ///  @  CreatedBy : TVTam(20/02/2023)
        /// </summary>
        /// <param name="id">Khóa chính dôid tượng cần xóa</param>
        /// <returns>200 thành công</returns>
        public int DeleteSevices(Guid id);

        /// <summary>
        /// Validate đối tượng 
        ///  @  CreatedBy : TVTam(20/02/2023)
        /// </summary>
        /// <param name="entity">đối tượng cần validate</param>
        /// <returns>
        /// true : thỏa mán validate
        /// Fasle : không thỏa mãn validate     
        /// </returns>
        public bool Validate(T entity);

        /// <summary>
        /// Sinh mã từ động 
        /// </summary>
        /// <param name="code">mã tài sản </param>
        /// <returns></returns>
        public string AutoCodeSevices();

    }
}
