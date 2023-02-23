using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.BLL.Interface;
using MISA.Common.Entity;
using MISA.DL.Interface;

namespace MISA.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AssetCategorysController : BaseController<fixed_asset_category>
    {
        private IAssetCategoryRepository _iassetCategoryRepository;
        private IAssetCategoryBLL _isssetCategoryBLL;

        public AssetCategorysController(IAssetCategoryRepository iassetCategoryRepository, IAssetCategoryBLL isssetCategoryBLL) : base(iassetCategoryRepository, isssetCategoryBLL)
        {
            _iassetCategoryRepository = iassetCategoryRepository;
            _isssetCategoryBLL = isssetCategoryBLL;
        }
    }
}
