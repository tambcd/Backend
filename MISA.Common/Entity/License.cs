using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.Entity
{
    public class license:BaseEntity
    {
        /// <summary>
        /// id chứng từ 
        /// </summary>
        public Guid license_id {get; set;}
        /// <summary>
        /// mã chứng từ 
        /// </summary>
        [MISARequired]
        public string license_code { get; set;}

        /// <summary>
        /// ngày bắt đầu sửa dụng 
        /// </summary>
        
        [MISARequired]
        public DateTime license_date {get; set;}
        /// <summary>
        /// ngày ghi tăng 
        /// </summary>
        
        [MISARequired]
        public DateTime increase_date {get; set;}

        /// <summary>
        /// tổng nguyên giá 
        /// </summary>
        /// 
        public double total_price {get; set;}
        /// <summary>
        /// mô tả chứng từ 
        /// </summary>
        public string note {get; set;}

        
        List<Guid> license_detail_ids {get; set;}


    }
}
