using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web07.HCSN.TUANTA.BL;
using MISA.Web07.HCSN.TUANTA.Common.Entity;
using MySqlConnector;
using System.Reflection.Metadata;

namespace MISA.Web07.HCSN.TUANTA.v2.BaseController
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field

        private IBaseBL<T> _baseBL;

        #endregion

        #region Constructor

        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        #region Method
        /// <summary>
        /// API Lấy tất cả Record
        /// </summary>
        /// <returns>Tất cả Record</returns>
        /// Created by: TUANTA (23/08/2022)

        [HttpGet]
        public IActionResult GetAllRecords()
        {
            try
            {
                var res = _baseBL.GetAllRecords();
                if(res != null)
                {
                    return StatusCode(StatusCodes.Status200OK, res);
                }    
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e002");
                }    
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// API Lấy 1 Record
        /// </summary>
        /// <returns>Thông tin Record</returns>
        /// Created by: TUANTA (23/08/2022)

        [HttpGet("GetRecord/{recordID}")]
        public IActionResult GetRecord(Guid recordID)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _baseBL.GetAllRecords());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
        }

        /// <summary>
        /// API Xóa 1 Record
        /// </summary>
        /// <param name="recordID">ID của Record muốn xóa</param>
        /// <returns>ID của Record vừa xóa</returns>
        /// Created by: TUANTA (23/08/2022)
        [HttpPost("delete")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                int res = _baseBL.Delete(id);
                if (res >= 1)
                {
                    //Trả data về client
                    return StatusCode(StatusCodes.Status200OK, res);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e002");
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="record">bản ghi cần thêm mới</param>
        /// <returns>bản ghi được thêm mới khi thành công</returns>
        /// Created by: TUANTA (25/08/2022)
        [HttpPost]
        public IActionResult InsertOneRecord([FromBody] T record)
        {
            try
            {
                var validateResult = HandleError.HandleError.IsValidate(ModelState);
                if (validateResult != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, validateResult);
                }

                var recordID = _baseBL.InsertOneRecord(record);

                if (recordID != Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status201Created, recordID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e004");
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        private IActionResult HandleException(Exception ex)
        {
            var error = new ErrorService();
            error.UserMsg = Resource.ResourceVN.Error_Exception;
            error.DevMsg = ex.Message;
            return StatusCode(500, error);
        }
        #endregion
    }
}
