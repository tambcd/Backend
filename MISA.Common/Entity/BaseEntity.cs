using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.Entity
{
    public class BaseEntity
    {
        /// <summary>
        /// Id của đơn vị
        /// </summary>
        public Guid? organization_id { get; set; }
        /// <summary>
        /// Ngày tạo 
        /// </summary>
        public DateTime? created_date { get; set; }
        /// <summary>
        /// người tạo 
        /// </summary>
        public string? created_by { get; set; }
        /// <summary>
        /// ngày thay đổi 
        /// </summary>
        public DateTime? modified_date { get; set; }
        /// <summary>
        /// người thay đổi
        /// </summary>

        public string? modified_by { get; set; }
    }
}
