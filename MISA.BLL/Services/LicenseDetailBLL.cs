using MISA.BLL.Interface;
using MISA.Common.Entity;
using MISA.DL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.BLL.Services
{
    public class LicenseDetailBLL : BaseBLL<license_detail>, ILicenseDetailBLL
    {
        ILicenseDetailRepository detailRepository;
        public LicenseDetailBLL(ILicenseDetailRepository _repository) : base(_repository)
        {
            detailRepository = _repository;
        }
        /// <summary>
        /// thêm tài sản theo chứng từ 
        /// </summary>
        /// <param name="id">id chứng từ </param>
        /// <param name="ids">danh sách id tài sản  </param>
        /// <returns>thất bại 0 || thành công =  số bản ghi </returns>
        public int insertLicenseDetail(Guid idLicense, List<Guid> idsAsset)
        {
             return detailRepository.InsertManyDetail(idLicense, idsAsset);
        }
    }
}
