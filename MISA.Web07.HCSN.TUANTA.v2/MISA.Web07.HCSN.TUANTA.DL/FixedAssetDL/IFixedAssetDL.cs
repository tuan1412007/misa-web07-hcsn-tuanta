using MISA.Web07.HCSN.TUANTA.Common.Entity;
using MISA.Web07.HCSN.TUANTA.Common.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.HCSN.TUANTA.DL
{
    public interface IFixedAssetDL:IBaseDL<FixedAsset>
    {
        /// <summary>
        /// Get mã tài sản mới nhất
        /// <returns>Mã tài sản  mới nhất</returns>
        /// Created by: TUANTA (05/09/2022)

        //string GetNewFixedAssetCode();
        public IEnumerable<GetNewFixedAssetCodeDTO> GetNewFixedAssetCode();


        /// <summary>
        /// Sửa thông tin tài sản
        /// </summary>
        /// <param name="fixedAssetID"> ID tài sản cần sửa</param>
        /// <param name="fixedAsset"></param>
        /// <returns>Mã tài sản vừa update</returns>
        /// Created by: TUANTA (18/08/2022)
        int UpdateFixedAsset(Guid fixedAssetID, FixedAsset fixedAsset);

        /// <summary>
        /// Lọc data theo phân loại và có phân trang
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentID"></param>
        /// <param name="fixedAssetCategoryID"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns>Danh sách tàn sản vừa lọc</returns>
        /// /// Created by: TUANTA (18/08/2022)
        public PagingData<FixedAsset> FilterFixedAsset(string? where, int pageSize = 10, int pageNumber = 1);

        /// <summary>
        /// Xoá nhiều tài sản
        /// </summary>
        /// <param name="fixedAssetID"></param>
        /// <returns></returns>
        /// Created by: TUANTA (05/09/2022)
        int DeleteFixedAssets(List<Guid> fixedAssetID);

        ///<summary>
        ///Lấy dữ liệu cho 1 record
        /// </summary>
        /// <param name="id""></param>
        /// <returns>Dữ liệu 1 record</returns>
        /// Created by: TUANTA (20/08/2022)
        public FixedAsset GetRecord(Guid id);
    }
}
