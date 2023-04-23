using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.Entity
{
    public class PagingRequest<T>
    {
        /// <summary>
        /// tổng số trang tính
        /// </summary>
        public int TotalPage { get; set; }
        /// <summary>
        /// tổng số bản ghi 
        /// </summary>
        public int TotalRecord { get; set; }

        /// <summary>
        /// trang hiện tại 
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// số bản ghi trên trang 
        /// </summary>
        public int CurrentPageRecords { get; set; }

        /// <summary>
        /// tổng số nguyên giá 
        /// </summary>
        public double TotalCost { get; set; }

        /// <summary>
        /// tổng số lượng 
        /// </summary>
        public double TotalQuantity { get; set; }

        /// <summary>
        /// tổng giá trị hao mòn 
        /// </summary>
        public double TotalDepreciationValue { get; set; }

        /// <summary>
        /// danh sách data phân trang 
        /// </summary>
        public List<T> Data { get; set; } = new List<T> { };
    }
}
