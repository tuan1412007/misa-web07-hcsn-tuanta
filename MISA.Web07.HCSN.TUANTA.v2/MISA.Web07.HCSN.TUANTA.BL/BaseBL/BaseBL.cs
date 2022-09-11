using MISA.Web07.HCSN.TUANTA.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.HCSN.TUANTA.BL
{
    public class BaseBL<T>:IBaseBL<T>
    {
        #region field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Contructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lấy toàn bộ Record
        /// </summary>
        /// <returns>Danh sách các Record</returns>
        /// Created by: TUANTA (23/08/2022)
        public IEnumerable<dynamic> GetAllRecords()
        {
            return _baseDL.GetAllRecords();
        }

        /// <summary>
        /// Lấy thông tin 1 Record
        /// </summary>
        /// <returns>Data 1 Record</returns>
        /// Created by: TUANTA (23/08/2022)
        public IEnumerable<dynamic> GetRecord(Guid recordID)
        {
            return _baseDL.GetRecord(recordID.ToString());
        }

        /// <summary>
        /// Xóa 1 Record
        /// </summary>
        /// <param name="id">ID của Record muốn xóa</param>
        /// <returns>ID Record vừa xóa</returns>
        /// Created by: TUANTA (23/08/2022)
        public int Delete(Guid id)
        {
            return _baseDL.Delete(id);
        }

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="record">bản ghi cần thêm mới</param>
        /// <returns>bản ghi được thêm mới khi thành công</returns>
        /// Created by: TUANTA (25/08/2022)
        public Guid InsertOneRecord(T record)
        {
            return _baseDL.InsertOneRecord(record);
        }
        #endregion
    }
}
