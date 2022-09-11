using MISA.Web07.HCSN.TUANTA.Common.Entity;
using MISA.Web07.HCSN.TUANTA.Common.Entity.DTO;
using MISA.Web07.HCSN.TUANTA.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Web07.HCSN.TUANTA.BL
{
    public class FixedAssetBL:BaseBL<FixedAsset>,IFixedAssetBL
    {
        #region Field

        private IFixedAssetDL _fixedAssetDL;

        #endregion

        #region Constructor

        public FixedAssetBL(IFixedAssetDL fixedAssetDL) : base(fixedAssetDL)
        {
            _fixedAssetDL = fixedAssetDL;
        }
        #endregion

        #region Method
        /// <summary>
        /// Get mã tài sản mới nhất
        /// <returns>Mã tài sản  mới nhất</returns>
        /// Created by: TUANTA (18/08/2022)
        public string GetNewFixedAssetCode()
        {
            //var fixedAssetCodeNew = "";
            //var fixedAssetCode = _fixedAssetDL.GetNewFixedAssetCode();

            //if (fixedAssetCode != "")
            //{
            //    var resultString = Regex.Match(fixedAssetCode.ToString(), @"\d{3}").Value;
            //    int number = int.Parse(resultString);
            //    number = number + 1;
            //    fixedAssetCodeNew = "TS" + number;
            //}
            //else
            //{
            //    fixedAssetCodeNew = "TS000";
            //}

            //return fixedAssetCodeNew;
                var fixedAssetCodeNew = "";
                var fixedassetCodes = _fixedAssetDL.GetNewFixedAssetCode();
                var results = new List<string>();
                var resultString = "";
                var isCheck = 0;
                var prefix = "TS";

                foreach (var fixedAssetCode in fixedassetCodes)
                {
                    var textFormat = Regex.Replace(fixedAssetCode.FixedAssetCode.ToString(), @"[\d-]", string.Empty);
                    results.Add(textFormat);
                    if (textFormat != "TS")
                    {
                        isCheck = isCheck + 1;
                    }

                }

                if (isCheck >= 2)
                {
                    foreach (var fixedAssetCode in fixedassetCodes)
                    {
                        if (Regex.Replace(fixedAssetCode.FixedAssetCode.ToString(), @"[\d-]", string.Empty) != "TS")
                        {
                            prefix = Regex.Replace(fixedAssetCode.FixedAssetCode.ToString(), @"[\d-]", string.Empty);
                            if (resultString == "")
                                resultString = fixedAssetCode.FixedAssetCode.ToString();
                        }

                    }
                }
                else
                {
                    foreach (var fixedAssetCode in fixedassetCodes)
                    {
                        if (Regex.Replace(fixedAssetCode.FixedAssetCode.ToString(), @"[\d-]", string.Empty) == "TS")
                        {
                            prefix = Regex.Replace(fixedAssetCode.FixedAssetCode.ToString(), @"[\d-]", string.Empty);
                            if (resultString == "")
                                resultString = fixedAssetCode.FixedAssetCode.ToString();
                        }
                    }
                }


                // tách chuỗi thành số
                if (resultString != "")
                {
                fixedAssetCodeNew = Regex.Match(resultString.ToString(), @"\d{3}").Value;
                    int number = int.Parse(fixedAssetCodeNew);
                    number = number + 1;
                    fixedAssetCodeNew = prefix + number;
                }
                else
                {
                    fixedAssetCodeNew = "TS00000";
                }

                return fixedAssetCodeNew;
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
        public PagingData<FixedAsset> FilterFixedAsset(string? keyword, Guid? fixedAssetCategoryID, Guid? departmentID, int pageSize = 10, int pageNumber = 1)
        {
            var orConditions = new List<string>();
            var andConditions = new List<string>();
            string whereClause = "";

            if (keyword != null)
            {
                orConditions.Add($"FixedAssetID LIKE '%{keyword}%'");
                orConditions.Add($"FixedAssetname LIKE '%{keyword}%'");
                orConditions.Add($"FixedAssetCode LIKE '%{keyword}%'");
            }
            if (orConditions.Count > 0)
            {
                whereClause = $"({string.Join(" OR ", orConditions)})";
            }

            if (fixedAssetCategoryID != null)
            {
                andConditions.Add($"FixedAssetCategoryID LIKE '%{fixedAssetCategoryID}%'");
            }
            if (departmentID != null)
            {
                andConditions.Add($"DepartmentID LIKE '%{departmentID}%'");
            }
            if (andConditions.Count > 0)
            {
                if (orConditions.Count > 0)
                {
                    whereClause += $"AND {string.Join(" AND ", andConditions)}";
                }
                else
                    whereClause += $" {string.Join(" AND ", andConditions)}";
            }
            return _fixedAssetDL.FilterFixedAsset(whereClause, pageSize, pageNumber);
        }

        /// <summary>
        /// Sửa thông tin tài sản
        /// </summary>
        /// <param name="fixedAssetID"> ID tài sản cần sửa</param>
        /// <param name="fixedAsset"></param>
        /// <returns>Mã tài sản vừa update</returns>
        /// Created by: TUANTA (18/08/2022)
        public int UpdateFixedAsset(Guid fixedAssetID, FixedAsset fixedAsset)
        {
            return (_fixedAssetDL.UpdateFixedAsset(fixedAssetID, fixedAsset));
        }

        /// <summary>
        /// Xoá nhiều tài sản
        /// </summary>
        /// <param name="fixedAssetID"></param>
        /// <returns></returns>
        /// Created by: TUANTA (05/09/2022)
        public int DeleteFixedAssets(List<Guid> fixedAssetID)
        {
            return _fixedAssetDL.DeleteFixedAssets(fixedAssetID);
        }

        ///<summary>
        ///Lấy dữ liệu cho 1 record
        /// </summary>
        /// <param name="id""></param>
        /// <returns>Dữ liệu 1 record</returns>
        /// Created by: TUANTA (20/08/2022)
        public FixedAsset GetRecord(Guid id)
        {
            return _fixedAssetDL.GetRecord(id);
        }
        #endregion
    }
}
