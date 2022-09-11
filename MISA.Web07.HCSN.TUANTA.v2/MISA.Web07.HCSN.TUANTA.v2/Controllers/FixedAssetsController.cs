using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web07.HCSN.TUANTA.BL;
using MISA.Web07.HCSN.TUANTA.Common.Entity;
using MISA.Web07.HCSN.TUANTA.v2.BaseController;

namespace MISA.Web07.HCSN.TUANTA.v2.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class FixedAssetsController : BasesController<FixedAsset>
    {
        #region Field

        private IFixedAssetBL _fixedAssetBL;

        #endregion

        #region Constructor

        public FixedAssetsController(IFixedAssetBL fixedAssetBL) : base(fixedAssetBL)
        {
            _fixedAssetBL = fixedAssetBL;
        }

        #endregion

        #region Method
        /// <summary>
        /// Get mã tài sản mới nhất
        /// <returns>Mã tài sản  mới nhất</returns>
        /// Created by: TUANTA (18/08/2022)
        [HttpGet("NewCodeFixedAsset")]
        public IActionResult GetNewFixedAssetCode()
        {
            try
            {
                var fixedAssetCodeNew = _fixedAssetBL.GetNewFixedAssetCode();
                return Ok(fixedAssetCodeNew);
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// Sửa thông tin tài sản
        /// </summary>
        /// <param name="fixedAssetID"> ID tài sản cần sửa</param>
        /// <param name="fixedAsset"></param>
        /// <returns>Mã tài sản vừa update</returns>
        /// Created by: TUANTA (18/08/2022)
        [HttpPut]
        public IActionResult UpdateFixedAsset(Guid fixedAssetID, [FromBody] FixedAsset fixedAsset)
        {
            try
            {


                var res = _fixedAssetBL.UpdateFixedAsset(fixedAssetID, fixedAsset);
                // 4 Trả về kết quả
                if (res > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, fixedAsset);
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
        /// Lọc data theo phân loại và có phân trang
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentID"></param>
        /// <param name="fixedAssetCategoryID"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns>Danh sách tàn sản vừa lọc</returns>
        /// Created by: TUANTA (18/08/2022)
        [HttpGet("Filter")]
        public IActionResult FilterFixedAsset([FromQuery] string? keyword, [FromQuery] Guid? fixedAssetCategoryID, [FromQuery] Guid? departmentID, [FromQuery] int pageSize = 20, [FromQuery] int pageNumber = 1)
        {
            var res = _fixedAssetBL.FilterFixedAsset(keyword, fixedAssetCategoryID, departmentID, pageSize, pageNumber);
            if (res != null)
            {
                return StatusCode(StatusCodes.Status200OK, res);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, res);
            }
        }

        private IActionResult HandleException(Exception ex)
        {
            var error = new ErrorService();
            error.UserMsg = Resource.ResourceVN.Error_Exception;
            error.DevMsg = ex.Message;
            return StatusCode(500, error);
        }

        /// <summary>
        /// Xoá nhiều tài sản
        /// </summary>
        /// <param name="fixedAssetID"></param>
        /// <returns></returns>
        /// Created by: TUANTA (05/09/2022)
        [HttpPost("deleteFixedAssets")]
        public IActionResult DeleteFixedAssets([FromBody] List<Guid> fixedAssetID)
        {
            try
            {
                var res = _fixedAssetBL.DeleteFixedAssets(fixedAssetID);
                if(res > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, res);
                }    
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, res);
                }    
                  
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        ///<summary>
        ///Lấy dữ liệu cho 1 record
        /// </summary>
        /// <param name="id""></param>
        /// <returns>Dữ liệu 1 record</returns>
        /// Created by: TUANTA (20/08/2022)
        [HttpGet("GetOneRecord")]
        public IActionResult GetRecord(Guid id)
        {
            try
            {
                var res = _fixedAssetBL.GetRecord(id);
                if(res != null)
                {
                    return StatusCode(StatusCodes.Status200OK, res);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e002");
                }
            }
            catch(Exception ex)
            {
                return HandleException(ex);
            }
        }
        #endregion
    }
}
