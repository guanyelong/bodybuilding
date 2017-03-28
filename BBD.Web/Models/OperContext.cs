using BBD.IBLL;
using BBD.Spring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace BBD.Web.Models
{
    public class OperContext
    {
        public IBLLSession iBllSession;
        private OperContext()
        {
            iBllSession = SpringHelper.GetObject<IBLLSession>("BLLSession");
        }

        public static OperContext CurrentContext
        {
            get
            {
                OperContext opContext = CallContext.GetData(typeof(OperContext).Name) as OperContext;
                if (opContext == null)
                {
                    opContext = new OperContext();
                    CallContext.SetData(typeof(OperContext).Name, opContext);
                }
                return opContext;
            }
        }
    }
}