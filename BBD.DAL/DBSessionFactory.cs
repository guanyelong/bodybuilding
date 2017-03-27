﻿using BBD.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BBD.DAL
{
    public class DBSessionFactory : IDBSessionFactory
    {

        public IDBSession GetDbSession()
        {
            IDBSession dbSession = CallContext.GetData(typeof(DBSessionFactory).Name) as DBSession;

            if (dbSession == null)
            {
                dbSession = new DBSession();
                CallContext.SetData(typeof(DBSessionFactory).Name, dbSession);
            }

            return dbSession;
        }
    }
}
