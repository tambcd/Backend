using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.BLL.Interface;
using MISA.Common.Entity;
using MISA.DL.Interface;

namespace MISA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : BaseController<fixed_asset> 
    {
        public AssetsController(IRepository<fixed_asset> epository, IBaseBLL<fixed_asset> baseBL) : base(epository, baseBL)
        {
        }
    }
}
