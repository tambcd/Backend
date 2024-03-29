﻿using Microsoft.AspNetCore.Http;
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
        ///   @created by : tvTam
        /// @create day : 19/3/2023
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
        /// Lấy danh sách tài sản theo chứng từ 
        /// </summary>
        /// <param name="id">khóa chính chứng từ </param>
        /// <returns></returns>
        [HttpGet("getByLicense")]
        public IActionResult GetByLicense( Guid id)
        {
            try
            {
                var data = _iassetRepository.GetByLicense(id);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }
        }
        /// <summary>
        /// xuất khẩu danh sách tài sản
        /// @created by : tvTam
        /// @create day : 19/3/2023
        /// </summary>
        /// <returns>danh sách tài sản sau khi đi fliter</returns>
        [HttpGet("Export")]
        public IActionResult Export(string? txtSearch, Guid? DepartmentId, Guid? AssetCategoryId)
        {
            try
            {
                var stream = _iassetBLL.ExportAssets(txtSearch, DepartmentId, AssetCategoryId);
                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }   
        }
        /// <summary>
        /// nhập khẩu từ excel
        /// @created by : tvTam
        /// @create day : 19/3/2023
        /// </summary>
        /// <param name="formFile">tệp chứa thông tin nhập khẩu </param>
        /// <returns>số bản ghi đươc nhập khẩu </returns>
        [HttpPost("Import")]
        public IActionResult Import(IFormFile formFile)
        {
            try
            {
                var path = _iassetBLL.ImportAssets(formFile);
                return Ok(path);
            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }

        }
        /// <summary>
        /// Cập nhập nguyên giá cho tài sản 
        ///@<param name="idAsset">id tài sản </param>
        /// <param name="idLicense">id chứng từ </param>
        /// <param name="cost">nuyên giá </param>
        /// <param name="new_cost">nguyên giá và nguồn chi phí </param>
        /// </summary>\
        /// <param name="assetUpdateCost">Thông tin cập nhập</param>
        /// 
        /// <returns></returns>

        [HttpPut("UpdateByCost")]
        public IActionResult UpdateByCost(AssetUpdateCost assetUpdateCost)
        {
            try
            {
                var path = _iassetBLL.UpdateCost(assetUpdateCost);
                return Ok(path);
            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }

        }


    }
}
