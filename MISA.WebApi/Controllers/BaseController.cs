using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.BLL.Interface;
using MISA.Common.Exceptions;
using MISA.DL.Interface;

namespace MISA.WebApi.Controllers
{
   
    [ApiController]
    public class BaseController <MISAEntity>: ControllerBase
    {
        protected IRepository<MISAEntity> _repository;
        protected IBaseBLL<MISAEntity> _baseBL;

        public BaseController(IRepository<MISAEntity> epository, IBaseBLL<MISAEntity> baseBL)
        {
            _repository = epository;
            _baseBL = baseBL;
        }


        /// <summary>
        /// Lấy tất cả bản ghi 
        /// </summary>
        /// <returns>Danh sách bản ghi của đối tượng</returns>
        /// createdby : TVTam(MF1270) 11/08/2022
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_repository.GetAll());
            }
            catch (Exception e)
            {
                return HandelException(e);
            }
        }
        /// <summary>
        /// Lấy phòng ban theo ID
        /// </summary>
        /// <param name="id"> khóa chính </param>
        /// <returns>1 đối tượng </returns>
        /// createdby : TVTam(MF1270) 11/08/2022
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var data = _repository.GetById(id);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }
        }
        /// <summary>
        /// Thêm phòng ban 
        /// </summary>
        /// <param name="entity"> đối tượng thêm mới</param>
        /// <returns>201 thành công ||Exception từng trường hợp </returns>
        /// createdby : TVTam(MF1270) 11/08/2022
        [HttpPost]
        public IActionResult Post(MISAEntity entity)
        {
            try
            {
                var data = _baseBL.InsertSevices(entity);
                return StatusCode(201, data);

            }
            catch (Exception ex)
            {

                return HandelException(ex);
            }
        }

        [HttpPut]
        public IActionResult Put(MISAEntity entity)
        {
            try
            {
                var data = _baseBL.UpdateSevices(entity);
                return StatusCode(200, data);

            }
            catch (Exception ex)
            {

                return HandelException(ex);
            }
        }
        /// <summary>
        /// Xóa đối tượng 
        /// </summary>
        /// <param name="id">Khóa  chính của đối tượng cần xóa</param>
        /// <returns>200 thành công</returns>
        /// createdby : TVTam(MF1270) 11/08/2022

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {

            try
            {
                var data = _baseBL.DeleteSevices(id);
                return Ok(data);

            }
            catch (Exception ex)
            {

                return HandelException(ex);
            }
        }

        /// <summary>
        /// Kiểm tra Exception
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>statuscode và lỗi </returns>
        /// 
        [NonAction]
        public IActionResult HandelException(Exception ex)
        {
            var res = new
            {
                devMsg = ex.Message,
                userMsg = Common.CommonResource.GetResoureString("ErrorSystem"),
                erros = ex.Data["ListValidate"],

            };
            if (ex is MISAException)
            {
                return BadRequest(res);
            }
            return StatusCode(500, res);

        }

        

    }
}
