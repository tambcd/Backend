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
        [MISARSame]
        [PropNameDisplay("license_code")]
        public string license_code { get; set;}

        /// <summary>
        /// ngày bắt đầu sửa dụng 
        /// </summary>
        
        [MISARequired]
        [PropNameDisplay("license_date")]
        public DateTime license_date {get; set;}
        /// <summary>
        /// ngày ghi tăng 
        /// </summary>
        
        [MISARequired]
        [PropNameDisplay("increase_date")]
        public DateTime increase_date {get; set;}

        /// <summary>
        /// tổng nguyên giá 
        /// </summary>
        /// 
        [MISARequired]
        [PropNameDisplay("total_price")]
        public double total_price {get; set;}
        /// <summary>
        /// mô tả chứng từ 
        /// </summary>
        public string note {get; set;}

        /// <summary>
        /// danh sách tài sản thuộc chứng từ 
        /// </summary>
        List<Guid> license_detail_ids {get; set;}


    }
}
