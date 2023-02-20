using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.Entity
{
    public class department: BaseEntity
    {
        /// <summary>
        /// id phong ban
        /// </summary>
        public Guid department_id { get; set; }

        /// <summary>
        /// mã phòng ban
        /// </summary>
        public string department_code { get; set;}

        /// <summary>
        /// tên phòng ban 
        /// </summary>
        public string department_name { get; set; }

        /// <summary>
        /// Mô tả 
        /// </summary>
        public string? description { get; set; }

        /// <summary>
        /// Có phải là cha không
        /// </summary>
        public int?  is_parent { get; set; }
        /// <summary>
        /// Id phòng ban cha
        /// </summary>
        public Guid? parent_id { get; set; }
     


    }
}
