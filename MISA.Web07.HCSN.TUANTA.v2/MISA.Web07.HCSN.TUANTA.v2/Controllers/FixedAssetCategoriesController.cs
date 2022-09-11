using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web07.HCSN.TUANTA.BL;
using MISA.Web07.HCSN.TUANTA.Common.Entity;
using MISA.Web07.HCSN.TUANTA.v2.BaseController;

namespace MISA.Web07.HCSN.TUANTA.v2.Controllers
{
    public class FixedAssetCategoriesController : BasesController<FixedAssetCategory>
    {
        #region Field

        private IFixedAssetCategoryBL _fixedAssetCategoryBL;

        #endregion

        #region Constructor

        public FixedAssetCategoriesController(IFixedAssetCategoryBL fixedAssetCategoryBL) : base(fixedAssetCategoryBL)
        {
            _fixedAssetCategoryBL = fixedAssetCategoryBL;
        }

        #endregion
    }
}
