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
        /// lấy ra danh sách phần tử phân trang và không thuộc list mã  
        /// </summary>
        /// <param name="codes">danh sách các ma</param>
        /// <param name="pageNumber">số trang</param>
        /// <param name="pageSize">số bản ghi trên trang </param>
        /// <param name="txtSearch">từ khóa tìm kiếm </param>
        /// <returns></returns>
        [HttpGet("getBySreach")]
        public IActionResult getBySreach(string codes, int pageNumber, int pageSize, string? txtSearch,Guid? idLicense)
        {
            try
            {
                
                var data = _repository.GetSreachBase(codes, pageNumber, pageSize, txtSearch, idLicense);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }
        }

        [HttpGet("getByItemSelect")]
        public IActionResult getByItemSelect(string? codes, int pageNumber, int pageSize, string? txtSearch)
        {
            try
            {
                var data = _repository.GetSelectItem(codes, pageNumber, pageSize, txtSearch);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }
        }
        /// <summary>
        /// Lấy tất cả bản ghi 
        /// @createdby : TVTam(MF1270) 21/02/2023
        /// <paramref name="ids"/> danh sách id tài sản ẩn 
        /// </summary>
        /// <returns>Danh sách bản ghi của đối tượng</returns>

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var data = _repository.GetAll();
                return Ok(data);
            }
            catch (Exception e)
            {
                return HandelException(e);
            }
        }
        [HttpPost("getActive")]
        public IActionResult GetRecord(List<Guid> ids)
        {
            try
            {
                var data = _repository.GetRecordActive(ids);
                return Ok(data);
            }
            catch (Exception e)
            {
                return HandelException(e);
            }
        }
        /// <summary>
        /// Lấy phòng ban theo ID
        /// @createdby : TVTam(MF1270) 21/02/2023
        /// </summary>
        /// <param name="id"> khóa chính </param>
        /// <returns>1 đối tượng </returns>
        /// 
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var data = _repository.GetById(id);
                if(data != null)
                {

                return StatusCode(200,data);

                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }
        }
        /// <summary>
        /// lấy bản ghi theo mã 
        /// </summary>
        /// <param name="code">mã đối tượng </param>
        /// <returns></returns>
        [HttpGet("getCode/{code}")]
        public IActionResult GetByCode(string code)
        {
            try
            {
                var data = _repository.GetByCode(code);
                if (data != null)
                {

                    return StatusCode(200, data);

                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }
        }
        /// <summary>
        /// Thêm phòng ban 
        /// @createdby : TVTam(MF1270) 21/02/2023
        /// </summary>
        /// <param name="entity"> đối tượng thêm mới</param>
        /// <returns>201 thành công ||Exception từng trường hợp </returns>
        [HttpPost]
        public IActionResult Post(MISAEntity entity)
        {
            try
            {
                var data = _baseBL.InsertSevices(entity);
                if (data ==1)
                {
                return StatusCode(201, data);
                }
                else { return BadRequest(); }

            }
            catch (Exception ex)
            {

                return HandelException(ex);
                
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, MISAEntity entity)
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
        /// @createdby : TVTam(MF1270) 21/02/2023
        /// </summary>
        /// <param name="id">Khóa  chính của đối tượng cần xóa</param>
        /// <returns>200 thành công</returns>

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
        /// xóa nhiều 
        /// </summary>
        /// <param name="ids">danh sách id xóa</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteMany(List<Guid> ids)
        {

            try
            {
                var data = _baseBL.DeleteManyService(ids);
                return Ok(data);

            }
            catch (Exception ex)
            {

                return HandelException(ex);
            }
        }
        /// <summary>
        /// xóa nhiều theo mã
        /// </summary>
        /// <param name="codes">danh sách mã</param>
        /// <returns></returns>
        [HttpDelete("ByCode")]
        public IActionResult DeleteManyBycode(List<string> codes)
        {

            try
            {
                var data = _repository.DeleteManyByCode(codes);
                return Ok(data);

            }
            catch (Exception ex)
            {

                return HandelException(ex);
            }
        }

        /// <summary>
        /// Get mã tài sản auto 
        /// </summary>
        /// <returns></returns>
        [HttpGet("NewAutoCode")]
        public IActionResult GetNewCode()
        {
            try
            {
                var data = _baseBL.AutoCodeSevices();
                return Ok(data);

            }
            catch (Exception ex)
            {
                return HandelException(ex);
            }
        }

        /// <summary>
        /// Kiểm tra Exception
        /// @createdby : TVTam(MF1270) 21/02/2023
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
