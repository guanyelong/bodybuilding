using BBD.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BBD.BLL
{
    public class BLLSessionFactory : IBLLSessionFactory
    {
        public IBLLSession GetBLLSesson()
        {
            IBLLSession bllSession = CallContext.GetData(typeof(BLLSessionFactory).Name) as BLLSession;

            if (bllSession == null)
            {
                bllSession = new BLLSession();
                CallContext.SetData(typeof(BLLSessionFactory).Name, bllSession);
            }

            return bllSession;
        }
    }
}
