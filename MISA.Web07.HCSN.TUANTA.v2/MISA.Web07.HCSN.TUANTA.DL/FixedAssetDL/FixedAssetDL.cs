using Dapper;
using MISA.Web07.HCSN.TUANTA.Common.Entity;
using MISA.Web07.HCSN.TUANTA.Common.Entity.DTO;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.HCSN.TUANTA.DL
{
    public class FixedAssetDL:BaseDL<FixedAsset>,IFixedAssetDL
    {
        /// <summary>
        /// Get mã tài sản mới nhất
        /// <returns>Mã tài sản  mới nhất</returns>
        /// Created by: TUANTA (05/9/2022)
        public IEnumerable<GetNewFixedAssetCodeDTO> GetNewFixedAssetCode()
        {
            //Khai báo và kết nối tới DB
            var connectionString = "Server=localhost;Port=3306;Database=misa_web07_hcsn_tuanta;Uid=root;Pwd=nhatdiepchithu1;";
            var mySqlConnection = new MySqlConnection(connectionString);

            //Lấy data
            //var mySqlCommand = "SELECT MAX(FixedAssetCode) FROM fixed_asset";
            //var fixedAssetCode = mySqlConnection.QueryFirst<object>(sql: mySqlCommand);
            //return fixedAssetCode.ToString();
            var mySqlCommand = "SELECT FixedAssetCode FROM fixed_asset ORDER BY ModifiedDate DESC LIMIT 0,2";
            var fixedAssetCode = mySqlConnection.Query<GetNewFixedAssetCodeDTO>(sql: mySqlCommand);

            return fixedAssetCode;
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
        /// /// Created by: TUANTA (18/08/2022)
        public PagingData<FixedAsset> FilterFixedAsset(string? where, int pageSize = 10, int pageNumber = 1)
        {
            //Khai báo và kết nối tới DB
            var connectionString = "Server=localhost;Port=3306;Database=misa_web07_hcsn_tuanta;Uid=root;Pwd=nhatdiepchithu1;";
            var mySqlConnection = new MySqlConnection(connectionString);

            //Lấy data
            var mySqlCommand = "Proc_Fixed_Asset_GetPaging";

            //CHuẩn bị tham số đầu vào cho proc
            var parameters = new DynamicParameters();
            parameters.Add("@v_Offset", (pageNumber - 1) * pageSize);
            parameters.Add("@v_Limit", pageSize);
            parameters.Add("@v_Sort", "ModifiedDate DESC");
            parameters.Add("@v_Where", where);

            //THục hiện gọi vào DB để run proc với tham số đầu vào như trên
            var multipleResults = mySqlConnection.QueryMultiple(mySqlCommand, parameters, commandType: System.Data.CommandType.StoredProcedure);

            //Xử lý kết kết quả trả về từ DB
            if (multipleResults != null)
            {
                var fixedAssets = multipleResults.Read<FixedAsset>().ToList();
                var totalCount = multipleResults.Read<long>().Single();
                var res = new PagingData<FixedAsset>()
                {
                    Data = fixedAssets,
                    TotalCount = totalCount
                };
                return res;
            }
            else
            {
                return new PagingData<FixedAsset>();
            }
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

            DateTime date = DateTime.Now;
            fixedAsset.modifiedDate = date;

            //Khai báo và kết nối tới DB
            var connectionString = "Server=localhost;Port=3306;Database=misa_web07_hcsn_tuanta;Uid=root;Pwd=nhatdiepchithu1;";
            var mySqlConnection = new MySqlConnection(connectionString);

            //Lấy data
            var mySqlCommand = "UPDATE fixed_asset SET FixedAssetName=@FixedAssetName,DepartmentID=@DepartmentID,FixedAssetCategoryID=@FixedAssetCategoryID,PurchaseDate=@PurchaseDate,Cost=@Cost,Quantity =@Quantity,DepreciationRate=@DepreciationRate,TrackedYear=@TrackedYear,LifeTime=@LifeTime,CreatedBy=@CreatedBy,CreatedDate=@CreatedDate,ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate,ProductionDate=@ProductionDate, DepreciationYear=@DepreciationYear WHERE FixedAssetID =@FixedAssetID";
            var parameters = new DynamicParameters();
            fixedAsset.fixedAssetID = fixedAssetID;
            parameters.Add("@FixedAssetID", fixedAsset.fixedAssetID);
            parameters.Add("@FixedAssetCode", fixedAsset.fixedAssetCode);
            parameters.Add("@FixedAssetName", fixedAsset.fixedAssetName);
            parameters.Add("@DepartmentID", fixedAsset.departmentID);
            parameters.Add("@FixedAssetCategoryID", fixedAsset.fixedAssetCategoryID);
            parameters.Add("@PurchaseDate", fixedAsset.purchaseDate);
            parameters.Add("@Cost", fixedAsset.cost);
            parameters.Add("@Quantity", fixedAsset.quantity);
            parameters.Add("@DepreciationRate", fixedAsset.depreciationRate);
            parameters.Add("@TrackedYear", fixedAsset.trackedYear);
            parameters.Add("@LifeTime", fixedAsset.lifeTime);
            parameters.Add("@CreatedBy", fixedAsset.createdBy);
            parameters.Add("@CreatedDate", fixedAsset.createdDate);
            parameters.Add("@ModifiedBy", fixedAsset.modifiedBy);
            parameters.Add("@ModifiedDate", fixedAsset.modifiedDate);
            parameters.Add("@ProductionDate", fixedAsset.productionDate);
            parameters.Add("@DepreciationYear", fixedAsset.depreciationYear) ;


            var res = mySqlConnection.Execute(sql: mySqlCommand, param: fixedAsset);
            return res;
        }

        /// <summary>
        /// Xoá nhiều tài sản
        /// </summary>
        /// <param name="fixedAssetID"></param>
        /// <returns></returns>
        /// Created by: TUANTA (05/09/2022)
        public int DeleteFixedAssets(List<Guid> fixedAssetID)
        {
            //Khai báo và kết nối tới DB
            var connectionString = "Server=localhost;Port=3306;Database=misa_web07_hcsn_tuanta;Uid=root;Pwd=nhatdiepchithu1;";
            var mySqlConnection = new MySqlConnection(connectionString);

            //
            var listFixedAsset = "";
            for(int i = 0; i < fixedAssetID.Count; i++)
            {
                if(i < fixedAssetID.Count-1)
                {
                    listFixedAsset = listFixedAsset + "'" + fixedAssetID[i].ToString() + "'" + ",";
                }    
                if(i == fixedAssetID.Count-1)
                {
                    listFixedAsset = listFixedAsset + "'" + fixedAssetID[i].ToString() + "'";
                }    
            } 
            
            //Get Data
            var mySqlCommand = $"DELETE FROM fixed_asset WHERE FixedAssetID IN({listFixedAsset.Replace("\"","")})";

            var numberRows = mySqlConnection.Execute(mySqlCommand);
            return numberRows;
        }

        ///<summary>
        ///Lấy dữ liệu cho 1 record
        /// </summary>
        /// <param name="id""></param>
        /// <returns>Dữ liệu 1 record</returns>
        /// Created by: TUANTA (20/08/2022)
        public FixedAsset GetRecord(Guid id)
        {
            //Khai báo và kết nối tới DB
            var connectionString = "Server=localhost;Port=3306;Database=misa_web07_hcsn_tuanta;Uid=root;Pwd=nhatdiepchithu1;";
            var mySqlConnection = new MySqlConnection(connectionString);

            //Get data
            var mySqlCommand = $"SELECT * FROM fixed_asset WHERE FixedAssetID = '{id}'";
            var parameter = new DynamicParameters();
            parameter.Add("@ID", id);
            var res = mySqlConnection.QueryFirstOrDefault<FixedAsset>(sql: mySqlCommand);
            return res;
        }
    }
}
