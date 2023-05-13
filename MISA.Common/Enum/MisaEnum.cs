using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.Enum
{
    /// <summary>
    /// thêm hay sửa 
    /// </summary>
    public enum MisaEnum
    {
        /// <summary>
        /// 1 thêm 
        /// </summary>
        Insert = 1,
        /// <summary>
        ///  sửa 
        /// </summary>
        Update = 2,
        /// <summary>
        /// mã lỗi trùng mã 
        /// </summary>
        ErrorCodeSameCode = 100,
        /// <summary>
        /// mã lỗi bỏ trống 
        /// </summary>
        ErrorCodeEmpty = 101,
        /// <summary>
        /// Mã lỗi danh sách rỗng
        /// </summary>
        ErrorCodelistEmpty = 102,
    }
}
