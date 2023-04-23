using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.BLL.Interface;
using MISA.Common.Entity;
using MISA.DL.Interface;

namespace MISA.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LicenseDetailsController : BaseController<license_detail>
    {
        ILicenseDetailRepository licenseDetailRepository;
        ILicenseDetailBLL licenseDetailBLL;
        public LicenseDetailsController(ILicenseDetailRepository epository, ILicenseDetailBLL baseBL) : base(epository, baseBL)
        {
            licenseDetailRepository = epository;
            licenseDetailBLL = baseBL;
        }
        /// <summary>
        /// thêm chứng từ 
        /// </summary>
        /// <param name="license">chứng từ </param>
        /// <param name="ids">danh sách id tài sản</param>
        /// <returns></returns>
        /*[HttpPost("DetailLicense")]
        public IActionResult InsertDetailLicense(Guid idLicense, List<Guid> ids)
        {
            try
            {
                var res = licenseDetailRepository.InsertManyDetail(idLicense, ids);
                if (res == 0)
                {
                    return StatusCode(404, res);
                }
                return StatusCode(201, res);
            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }

        }*/
    }
}
