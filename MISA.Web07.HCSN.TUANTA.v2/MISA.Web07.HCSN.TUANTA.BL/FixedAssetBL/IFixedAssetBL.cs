using MISA.Web07.HCSN.TUANTA.Common.Entity;
using MISA.Web07.HCSN.TUANTA.Common.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.HCSN.TUANTA.BL
{
    public interface IFixedAssetBL:IBaseBL<FixedAsset>
    {
        /// <summary>
        /// Get mã tài sản mới nhất
        /// <returns>Mã tài sản  mới nhất</returns>
        /// Created by: TUANTA (18/08/2022)
        public string GetNewFixedAssetCode();

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
        public PagingData<FixedAsset> FilterFixedAsset(string? keyword, Guid? fixedAssetCategoryID, Guid? departmentID, int pageSize = 10, int pageNumber = 1);

        /// <summary>
        /// Sửa thông tin tài sản
        /// </summary>
        /// <param name="fixedAssetID"> ID tài sản cần sửa</param>
        /// <param name="fixedAsset"></param>
        /// <returns>Mã tài sản vừa update</returns>
        /// Created by: TUANTA (18/08/2022)
        public int UpdateFixedAsset(Guid fixedAssetID, FixedAsset fixedAsset);

        /// <summary>
        /// Xoá nhiều tài sản
        /// </summary>
        /// <param name="fixedAssetID"></param>
        /// <returns></returns>
        /// Created by: TUANTA (05/09/2022)
        public int DeleteFixedAssets(List<Guid> fixedAssetID);

        ///<summary>
        ///Lấy dữ liệu cho 1 record
        /// </summary>
        /// <param name="id""></param>
        /// <returns>Dữ liệu 1 record</returns>
        /// Created by: TUANTA (20/08/2022)
        public FixedAsset GetRecord(Guid id);
    }
}
