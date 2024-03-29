﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.Entity
{
    public class fixed_asset : BaseEntity
    {
        /// <summary>
        /// Id tài sản
        /// </summary>
        [PropNameDisplay("fixed_asset_id")]
        public Guid? fixed_asset_id { get; set; }
        /// <summary>
        /// Mã tài sản
        /// </summary>
        /// 
        [MISARequired]
        [MISARSame]
        [PropNameDisplay("AssetCode")]
        public string fixed_asset_code { get; set; }
        /// <summary>
        /// Tên tài sản
        /// </summary>
        /// 
        [MISARequired]
        [PropNameDisplay("AssetName")]
        public string? fixed_asset_name { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public string? organization_code { get; set; }
        /// <summary>
        /// 'Tên của đơn vị
        /// </summary>
        public string? organization_name { get; set; }
        /// <summary>
        /// Id phòng ban
        /// </summary>
        public Guid? department_id { get; set; }
        /// <summary>
        /// 'Mã phòng ban
        /// </summary>
        /// 
        [MISARequired]
        [PropNameDisplay("Mã phòng ban")]
        public string? department_code { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// 
        public string? department_name { get; set; }
        /// <summary>
        /// Id loại tài sản
        /// </summary>
        public Guid fixed_asset_category_id { get; set; }
        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        /// 
        [MISARequired]
        [PropNameDisplay("Mã loại tài sản")]
        public string? fixed_asset_category_code { get; set; }
        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string? fixed_asset_category_name { get; set; }
        /// <summary>
        /// 'Ngày mua
        /// </summary>
        public DateTime purchase_date { get; set; }
        /// <summary>
        /// nguyên giá
        /// </summary>
        /// 
        [MISARequired]
        [MISANumberBig]
        [PropNameDisplay("Cost")]
        public double cost { get; set; }
        public string cost_new { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        /// 
        [MISARequired]
        [PropNameDisplay("quantity")]
        public int quantity { get; set; }
        /// <summary>
        /// Giá trị hao mòn năm
        /// </summary>
        /// 
        [MISARequired]
        [PropNameDisplay("DepreciationValue")]
        public double depreciation_value { get; set; }
        /// <summary>
        /// Tỷ lệ hao mòn (%)
        /// </summary>
        /// 
        [MISARequired]
        [PropNameDisplay("Tỷ lệ hao mòn (%)")]
        public double depreciation_rate { get; set; }
        /// <summary>
        /// Năm bắt đầu theo dõi tài sản trên phần mềm
        /// </summary>
        /// 
        [MISARequired]
        public int tracked_year { get; set; }
        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        /// 
        [MISARequired]
        [PropNameDisplay("Số năm sử dụng")]
        public int life_time { get; set; }
        /// <summary>
        /// Năm sử dụng
        /// </summary>
        /// 
        [MISARequired]
        [PropNameDisplay("Ngay bắt đầu sử dụng")]
        public DateTime production_year { get; set; }
        /// <summary>
        /// Sử dụng
        /// </summary>
        public bool active { get; set; }

        /// <summary>
        /// lưu trữ lỗi validate
        /// </summary>
        public List<string>? ListerroImport { get; set; } = new List<string>();

        /// <summary>
        /// Tổng só bản ghi khi paging 
        /// </summary>
        public int? TotalRecord { get; set; }

        /// <summary>
        /// Tổng nguyên gía 
        /// </summary>
        public double totalCost { get; set; }

        /// <summary>
        /// tổng giá trị hao mòn lũy kế
        /// </summary>
        public double TotalDepreciationValue { get; set; }

        /// <summary>
        /// Tổng số lượng tài sản
        /// </summary>
        public double TotalQuantity { get; set; }

        /// <summary>
        /// Giá trị còn lại
        /// </summary>
        public double value_end { get; set; }

        /// <summary>
        /// Mã chứng từ 
        /// </summary>
        public string license_code { get; }



    }
}
