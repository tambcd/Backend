using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.Entity
{
    public class license_detail:BaseEntity
    {
        /// <summary>
        /// khóa chính chi tiết chứng từ 
        /// </summary>
        public Guid license_detail_id { get; set; }

        /// <summary>
        /// khóa chính tài sản 
        /// </summary>
        public Guid fixed_asset_id { get; set; }   
        /// <summary>
        /// khóa chính chứng từ 
        /// </summary>
        public Guid license_id { get; set; }
    }
}
