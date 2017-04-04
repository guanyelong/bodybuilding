using BBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.IBLL
{
    public partial interface Itb_Weight_Chg_Self_Bo_BLL
    {
        IList<tb_Weight_Chg_Self> GetAppWeightSelfList(int pageIndex, int pageSize, ref int count, tb_Weight_Chg_Self info);
    }
}
