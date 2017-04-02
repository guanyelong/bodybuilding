using BBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.IBLL
{
    public partial interface Itb_Serv_POF_Bo_BLL
    {
        IList<tb_Serv_Info> GetAppServList(int pageIndex, int pageSize, ref int count, tb_Serv_Info info);
    }
}
