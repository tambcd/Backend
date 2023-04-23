using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.Entity
{
    public class cost_source
    {
        /// <summary>
        /// khóa chính đơn vị cấp kinh phí 
        /// </summary>
        public Guid cost_source_id { get; set; }

        /// <summary>
        /// mã đơn vị cấp kinh phí 
        /// </summary>
        public string cost_source_code { get; set; }
        /// <summary>
        /// tên đơn vị cấp kinh phí 
        /// </summary>
        public string cost_source_name { get; set; }

    }
}
