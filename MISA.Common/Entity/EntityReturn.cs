using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.Entity
{
    public class EntityReturn
    {
        /// <summary>
        /// trạng thái lỗi 
        /// </summary>
        public int statusCode { get; set; }
        /// <summary>
        /// list mã lỗi 
        /// </summary>
        public List<int> codeError { get; set; } = new List<int>();
        /// <summary>
        /// danh sach lỗi
        /// </summary>
        public List<string> titleError { get; set; } = new List<string>();
        /// <summary>
        /// thong tin lỗi 
        /// </summary>
        public string devMsg { get; set; }
    }
}
