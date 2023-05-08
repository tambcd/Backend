using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.BLL.Interface;
using MISA.Common.Entity;
using MISA.DL.Interface;

namespace MISA.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LicensesController : BaseController<license>
    {
        ILicenseRepository licenseRepository;
        ILicenseBLL licenseBLL;
        public LicensesController(ILicenseRepository epository, ILicenseBLL baseBL) : base(epository, baseBL)
        {
            licenseRepository = epository;
            licenseBLL = baseBL;
        }
        /// <summary>
        /// thêm chứng từ và danh sách tài sản thuộc chứng từ 
        /// </summary>
        /// <param name="licenseAsset">đối tượng chứa chứng từ và danh sách id tài sản </param>
        /// <returns></returns>
        [HttpPost("Detail")]
        public IActionResult InsertDetailLicense(LicenseAsset licenseAsset)
        {
            try
            {
                var res = licenseBLL.InsertLicense(licenseAsset.license, licenseAsset.ids);
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

        }

        /// <summary>
        /// thêm chứng từ và danh sách tài sản thuộc chứng từ 
        /// </summary>
        /// <param name="licenseAsset">đối tượng chứa chứng từ và danh sách id tài sản </param>
        /// <returns></returns>
        [HttpPut("Detail")]
        public IActionResult UpdateDetailLicense(EntityUpdateLicense licenseAsset)
        {
            try
            {
                var res = licenseBLL.UpdateLicense(licenseAsset);
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

        }

    }
}
