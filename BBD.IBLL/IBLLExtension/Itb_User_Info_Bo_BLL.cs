using BBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.IBLL
{
    public partial interface Itb_User_Info_Bo_BLL
    {
        IList<tb_User_Info> GetAppUserList(int pageIndex, int pageSize, ref int count, tb_User_Info info);
    }
}
