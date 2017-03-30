using BBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.IBLL
{
    public partial interface Itb_Hosp_Info_Bo_BLL
    {
        IList<tb_Hosp_Info> GetAppHospList(int pageIndex, int pageSize, ref int count, tb_Hosp_Info info);
    }
}
