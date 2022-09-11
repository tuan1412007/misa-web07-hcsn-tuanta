using MISA.Web07.HCSN.TUANTA.Common.Entity;
using MISA.Web07.HCSN.TUANTA.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.HCSN.TUANTA.BL
{
    public class FixedAssetCategoryBL:BaseBL<FixedAssetCategory>, IFixedAssetCategoryBL
    {
        #region Field

        private IFixedAssetCategoryDL _fixedAssetCategoryDL;

        #endregion

        #region Constructor

        public FixedAssetCategoryBL(IFixedAssetCategoryDL fixedAssetCategoryDL) : base(fixedAssetCategoryDL)
        {
            _fixedAssetCategoryDL = fixedAssetCategoryDL;
        }
        #endregion
    }
}
