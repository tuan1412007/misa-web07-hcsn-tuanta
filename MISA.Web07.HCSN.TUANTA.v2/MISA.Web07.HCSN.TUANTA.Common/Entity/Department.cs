﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.HCSN.TUANTA.Common.Entity
{
    public class Department
    {
        [Key("departmentID")]
        /// <summary>
        /// ID bộ phận sử dụng
        /// </summary>
        /// 
        [Required]
        public Guid departmentID { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Mã bộ phận sử dụng
        /// </summary>
        /// 
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the name")]
        [StringLength(maximumLength: 50, MinimumLength = 10, ErrorMessage = "Length must be between 10 to 50")]
        public string departmentCode { get; set; }
        /// <summary>
        /// tên bộ phận sử dụng
        /// </summary>
        /// 
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the name")]
        [StringLength(maximumLength: 100, MinimumLength = 10, ErrorMessage = "Length must be between 10 to 100")]
        public string departmentName { get; set; }
        /// <summary>
        /// mô tả
        /// </summary>
        public string note { get; set; }
        /// <summary>
        /// người tạo
        /// </summary>
        public string createdBy { get; set; }
        /// <summary>
        /// ngày tạo
        /// </summary>
        public DateTime createdDate { get; set; }
        /// <summary>
        /// người chỉnh sửa
        /// </summary>
        public string modifiedBy { get; set; }
        /// <summary>
        /// ngày chỉnh sửa
        /// </summary>
        public DateTime modifiedDate { get; set; }
    }
}
