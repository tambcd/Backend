using MISA.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.DL.Interface
{
    public interface ILicenseDetailRepository: IRepository<license_detail>
    {
        /// <summary>
        /// thêm tài sản theo chứng từ 
        /// </summary>
        /// <param name="id">id chứng từ </param>
        /// <param name="ids">danh sách id tài sản  </param>
        /// <returns>thất bại 0 || thành công =  số bản ghi </returns>
        public int InsertManyDetail(Guid id, List<Guid> ids);

    }
}
