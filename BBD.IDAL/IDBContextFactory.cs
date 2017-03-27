using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBD.IDAL
{
    public interface IDBContextFactory
    {
        /// <summary>
        /// 获取EF上下文
        /// </summary>
        /// <returns></returns>
        DbContext GetDbContext();
    }
}
