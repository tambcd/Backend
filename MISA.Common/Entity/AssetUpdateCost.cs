using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.Entity
{
    public class AssetUpdateCost
    {
        /// <summary>
        /// mã tài sản 
        /// </summary>
        [PropNameDisplay("fixed_asset_id")]
        [MISARequired]
        public Guid idAsset { get; set; }
        /// <summary>
        /// mã chứng từ 
        /// </summary>
        [PropNameDisplay("license_id")]
        [MISARequired]
        public Guid idLicense { get; set; }
        /// <summary>
        /// nguyên giá 
        /// </summary>
        /// 
        [PropNameDisplay("Cost")]
        public double cost { get; set; }
        /// <summary>
        /// Nguồn nguyên giá
        /// </summary>
        /// 
        [PropNameDisplay("costS")]
        [MISARequired]
        public string new_cost { get; set; }
    }
}
