using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.BLL.Interface;
using MISA.Common.Entity;
using MISA.DL.Interface;

namespace MISA.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CostCourcesController : BaseController<cost_source>
    {
        private ICostCourceRepository _iCostCourceRepository;
        private  ICostSourceBLL _costSourceBLL;
        public CostCourcesController(ICostCourceRepository epository, ICostSourceBLL baseBL) : base(epository, baseBL)
        {
            _iCostCourceRepository = epository;
            _costSourceBLL = baseBL;
        }
    }
}
