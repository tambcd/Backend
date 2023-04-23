using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.Entity
{
    public class LicenseAsset
    {
        /// <summary>
        /// cchứng từ 
        /// </summary>
        public license license { get; set; }
        /// <summary>
        /// danh sách id tài sản 
        /// </summary>
        public List<Guid> ids { get; set; }    
    }
}
