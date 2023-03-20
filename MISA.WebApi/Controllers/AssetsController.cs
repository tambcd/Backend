using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.BLL.Interface;
using MISA.Common.Entity;
using MISA.DL.Interface;

namespace MISA.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AssetsController : BaseController<fixed_asset>
    {
        private IAssetRepository _iassetRepository;
        private IAssetBLL _iassetBLL;

        public AssetsController (IAssetRepository iassetRepository, IAssetBLL iassetBLL) : base(iassetRepository, iassetBLL)
        {
            _iassetRepository = iassetRepository;
            _iassetBLL = iassetBLL;
        }
        /// <summary>
        ///   tìm kiếm phân trang theo tiêu chí 
        /// </summary>
        /// <param name="pageNumber">số trang hiện tại </param>
        /// <param name="pageSize">số bản ghi trên 1 trang </param>
        /// <param name="txtSearch">từ khóa tìm kiếm </param>
        /// <param name="DepartmentId">id phòng ban  </param>
        /// <param name="AssetCategoryId">id loại tài sản </param>
        /// <returns></returns>

        [HttpGet("getByFilter")]
        public IActionResult GetFilter(int pageNumber, int pageSize, string? txtSearch,Guid? DepartmentId,Guid? AssetCategoryId)
        {
            try
            {
                var data = _iassetRepository.GetFilter(pageNumber, pageSize, txtSearch, DepartmentId, AssetCategoryId);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }
        }

        
        /// <summary>
        /// xuất khẩu danh sách tài sản
        /// </summary>
        /// <returns></returns>
        [HttpGet("Export")]
        public IActionResult Export()
        {
            try
            {
                var stream = _iassetBLL.ExportAssets();
                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }
        }

    }
}
