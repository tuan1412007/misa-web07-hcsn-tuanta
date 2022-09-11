using Microsoft.AspNetCore.Mvc.ModelBinding;
using MISA.Web07.HCSN.TUANTA.Common.Entity;

namespace MISA.Web07.HCSN.TUANTA.v2.HandleError
{
    public class HandleError
    {
        public static ErrorService? IsValidate(ModelStateDictionary modelState)
        {

            if (!modelState.IsValid)
            {
                // Khởi tạo 1 danh sách lưu lỗi
                var errors = new List<string>();
                foreach (var item in modelState)
                {
                    foreach (var error in item.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                var errorsData = new List<string>();
                foreach (var error in errors)
                {
                    // Gán thông báo cho người dùng

                    if (error == "e006")
                    {
                        errorsData.Add(Resource.ResourceValidate.FixedAssetCode);
                    }
                    if (error == "e007")
                    {
                        errorsData.Add(Resource.ResourceValidate.FixedAssetName);
                    }
                    if (error == "e008")
                    {
                        errorsData.Add(Resource.ResourceValidate.DepartmentID);
                    }
                    if (error == "e009")
                    {
                        errorsData.Add(Resource.ResourceValidate.FixedAssetCategoryID);
                    }

                }
                var errorResult = new ErrorService();
                errorResult.DevMsg = Resource.ResourceVN.Error_ValidateData;
                errorResult.UserMsg = "Nhập dữ liệu đầy đủ";
                errorResult.data = errorsData;

                return errorResult;
            }
            return null;
        }
    }
}
