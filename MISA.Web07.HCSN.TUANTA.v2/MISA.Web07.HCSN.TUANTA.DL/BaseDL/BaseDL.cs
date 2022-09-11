using Dapper;
using MISA.Web07.HCSN.TUANTA.Common.Entity;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.HCSN.TUANTA.DL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        #region Field
        private const string CONNECTING = "Server=localhost;Port=3306;Database=misa_web07_hcsn_tuanta;Uid=root;Pwd=nhatdiepchithu1;";
        #endregion

        ///<summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns> Danh sách tất cả bản ghi</returns>
        /// Created By: TUANTA (23/08/2022)
        public virtual IEnumerable<dynamic> GetAllRecords()
        {
            using (var mySqlConnection = new MySqlConnection(CONNECTING))
            {
                // lấy className của Table
                var name = GetTableName();

                //Lấy data qua truy vấn select
                var mySqlCommand = $"SELECT * FROM {name} ORDER BY CreatedDate ASC";
                var res = mySqlConnection.Query(sql: mySqlCommand);

                //Trả data
                return res;
            }
        }

        /// <summary>
        /// Lấy thông tin 1 bản ghi
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn xem</param>
        /// <returns>Thông tin 1 bản ghi</returns>
        /// Created by: TUANTA (23/08/2022)
        public virtual IEnumerable<dynamic> GetRecord(string recordID)
        {
            using (var mySqlConnection = new MySqlConnection(CONNECTING))
            {
                var tableObj = typeof(T).GetCustomAttribute<TableAttribute>();
                var keyObj = typeof(T).GetCustomAttribute<MISA.Web07.HCSN.TUANTA.Common.Entity.KeyAttribute>();
                if (tableObj != null && keyObj != null)
                {
                    //Chuẩn bị câu truy vấn SELECT
                    var tableName = tableObj.Name;
                    var fieldName = keyObj.Key;

                    string getRecordCommand = $"SELECT * FROM {tableName} WHERE {fieldName} = '{recordID}'";

                    //Thực hiện chạy câu lệnh SELECT
                    var records = mySqlConnection.Query(getRecordCommand);
                    return records;
                }
                else
                {
                    return Enumerable.Empty<dynamic>();
                }
            }
        }

        ///<summary>
        ///Lấy ClassName của table tương ứng
        /// </summary>
        /// <returns>
        /// Tên bảng
        /// </returns>
        /// Created by: TUANTA(20/08/2022)
        private string GetTableName()
        {
            var isDefineAttributeTableName = Attribute.IsDefined(typeof(T), typeof(TableName));
            if (isDefineAttributeTableName)
            {
                var attribute = Attribute.GetCustomAttribute(typeof(T), typeof(TableName));
                var tableName = (attribute as TableName).Name; //đưa về Attribute TableName
                return tableName;
            }
            else
            {
                return typeof(T).Name;
            }
        }

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn xóa</param>
        /// <returns>ID của bản ghi vừa xóa</returns>
        /// Created by: TUANTA (23/08/2022)
        public int Delete(Guid id)
        {
            using (var mySqlConnection = new MySqlConnection(CONNECTING))
            {
                //Lấy CLassName tương ứng
                var tablename = GetTableName();
                //Truy vấn data
                var attribute = Attribute.GetCustomAttribute(typeof(T), typeof(KeyAttribute));
                var valuekey = (attribute as KeyAttribute).Key;
                var mySqlCommand = $"DELETE FROM {tablename} WHERE {valuekey}=@ID";
                var parameters = new DynamicParameters();
                parameters.Add("@ID", id);
                var res = mySqlConnection.Execute(sql: mySqlCommand, param: parameters);
                return res;
            }
        }

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="record">Đối tượng bản ghi cần thêm mới</param>
        /// <returns>Trả về bản ghi khi thêm mới thành công</returns>
        /// Created by: TUANTA (25/08/2022)

        public Guid InsertOneRecord(T record)
        {
            // khai báo tên bảng và Store Procedure
            string tableName = GetTableName();
            string insertStoredProcedureName = $"Proc_{tableName}_InsertOne";

            //CHuẩn bị tham số đầu vào cho Proc
            var properties = typeof(T).GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                string propertyName = $"{property.Name}";
                var propertyValue = property.GetValue(record); // đọc vào object record -> lấy được value của property  tên là property + name
                parameters.Add(propertyName, propertyValue);
            }

            //Gọi vào DB để chạy Proc với tham số đầu vào đã khai báo như trên

            using (var mySqlConnection = new MySqlConnection(CONNECTING))
            {
                var numberOfAffctedRows = mySqlConnection.Execute(insertStoredProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                var result = Guid.Empty;
                if (numberOfAffctedRows > 0)
                {
                    //var primaryKeyProperty = typeof(T).GetProperties().FirstOrDefault(prop => prop.GetCustomAttributes(typeof(KeyAttribute), true).Count() > 0);
                    //var newId = primaryKeyProperty?.GetValue(record);
                    //if(newId != null)
                    //{
                    //    result = (Guid)newId;
                    //}
                    result = (Guid)properties[0].GetValue(record);
                }
                return result;
            }
        }

        ///<summary>
        ///Sửa 1 bản ghi
        /// </summary>
        ///<param name="Record"> Bản ghi cần thêm mới </param>
        ///<param name="RecordId"></param>
        ///<returns> Trả về bản ghi khi đã sửa thành công </returns>
        public Guid UpdateOneRecord(T record, Guid RecordId)
        {
            // khai báo tên bảng và Store Procedure
            string tableName = GetTableName();
            string updateStoredProcedureName = $"Proc_{tableName}_Update";

            //CHuẩn bị tham số đầu vào cho Proc
            var properties = typeof(T).GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var propertyName = $"{property.Name}";
                var propertyValue = property.GetValue(record);
                parameters.Add(propertyName, propertyValue);
            }

            using (var mySqlConnection = new MySqlConnection(CONNECTING))
            {
                var numberOfAffctedRows = mySqlConnection.Execute(updateStoredProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                var result = Guid.Empty;
                if (numberOfAffctedRows > 0)
                {
                    result = (Guid)properties[0].GetValue(record);
                }
                return result;
            }
        }
    }
}
