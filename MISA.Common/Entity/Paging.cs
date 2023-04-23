using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.Entity
{
    public class Paging
    {
        /// <summary>
        /// tổng số bản ghi 
        /// </summary>
        public int TotalRecord { get; set; }

        /// <summary>
        /// tổng nguyên giá 
        /// </summary>
        public double TotalCost { get; set; }
        /// <summary>
        /// tổng số lượng 
        /// </summary>
        public double TotalQuantity { get; set; }

        /// <summary>
        ///  tổng giá trị hao mòn 
        /// </summary>
        public double TotalDepreciationValue { get; set; }
    }
}
