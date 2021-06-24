using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums.FAMMS.Inventory.ItemMasterData
{
    public enum ItemMasterDataStatusEnum
    {
        [Description("Active")]
        Active = 1,

        [Description("InActive")]
        InActive = 2,

        [Description("Advanced")]
        Advanced = 3,
    }
}
