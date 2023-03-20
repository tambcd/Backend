using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.Entity
{
    public class fixed_asset_category : BaseEntity
    {
        /// <summary>
        /// Id loại tài sản
        /// </summary>
        public Guid fixed_asset_category_id { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        public string fixed_asset_category_code { get; set; }
        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string fixed_asset_category_name { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn (%)
        /// </summary>
        public double depreciation_rate { get; set; }
        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        public int? life_time { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? description { get; set; }

    }
}
