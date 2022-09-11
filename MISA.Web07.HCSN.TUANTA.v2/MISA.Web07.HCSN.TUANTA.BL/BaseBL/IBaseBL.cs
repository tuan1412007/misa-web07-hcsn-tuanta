using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.HCSN.TUANTA.BL
{
    public interface IBaseBL<T>
    {
        /// <summary>
        /// Lấy toàn bộ Record
        /// </summary>
        /// <returns>Danh sách các Record</returns>
        /// Created by: TUANTA (23/08/2022)
        public IEnumerable<dynamic> GetAllRecords();


        /// <summary>
        /// Lấy 1 Record
        /// </summary>
        /// <param name="recordID"></param> 
        /// <returns>Data 1 Record</returns>
        /// Created by: TUANTA (23/08/2022)
        public IEnumerable<dynamic> GetRecord(Guid recordID);

        /// <summary>
        /// Xóa 1 Record
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>ID Record vừa xóa</returns>
        /// Created by: TUANTA (23/08/2022)
        public int Delete(Guid id);

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="record">bản ghi cần thêm mới</param>
        /// <returns>bản ghi được thêm mới khi thành công</returns>
        /// Created by: TUANTA (25/08/2022)
        public Guid InsertOneRecord(T record);
    }
}
