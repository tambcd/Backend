using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.BLL.Interface;
using MISA.Common.Entity;
using MISA.DL.Interface;

namespace MISA.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseController<department>
    {
        private IDepartmentRepository _departmentRepository;
        private IDepartmentBLL _departmentBL;
        public DepartmentsController(IDepartmentRepository departmentRepository, IDepartmentBLL departmentBL) : base(departmentRepository, departmentBL)
        {
            _departmentRepository = departmentRepository;
            _departmentBL = departmentBL;
        }

    }
}
