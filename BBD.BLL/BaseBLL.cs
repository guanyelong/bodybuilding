using BBD.IBLL;
using BBD.IDAL;
using BBD.Spring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BBD.BLL
{
    public abstract class BaseBLL<T> : IBaseBLL<T> where T : class,new()
    {
        //EF上下文
        //不能直接实例化，否则仓储层与服务层之间会产生紧耦合
        //protected IBaseDAL<T> idal = new DALMsSql.BaseDAL<T>();

        protected IBaseDAL<T> idal;
        public abstract void SetiDal();


        public BaseBLL()
        {
            SetiDal();
        }

        //初始化仓储对象
        private IDBSession iDbSession;

        public IDBSession DbSession
        {
            get
            {
                if (iDbSession == null)
                {
                    IDBSessionFactory dbSessionFactory = SpringHelper.GetObject<IDBSessionFactory>("DBSessionFactory");
                    iDbSession = dbSessionFactory.GetDbSession();
                }

                return iDbSession;
            }

        }
        #region 数据库名称
        public string DbName
        {
            set
            {
                idal.DbName = value;
            }
        }
        #endregion

        #region 新增
        public int Add(T model)
        {
            return idal.Add(model);
        }

        #endregion

        #region 根据Id删除
        public int Del(T model)
        {
            return idal.Del(model);
        }
        #endregion

        #region 根据条件删除
        public int DelBy(Expression<Func<T, bool>> delWhere)
        {
            return idal.DelBy(delWhere);
        }
        #endregion

        #region 修改
        public int Modify(T model, params string[] proNames)
        {
            return idal.Modify(model, proNames);
        }

        public int Modifyed(T model, params string[] proNames)
        {
            return idal.Modifyed(model, proNames);
        }
        public int Modify(T model, params  Expression<Func<T, object>>[] ignorePerperties)
        {
            return idal.Modify(model, ignorePerperties);
        }
        #endregion

        #region 批量修改
        public int ModifyBy(T model, Expression<Func<T, bool>> whereLambda, params string[] proNames)
        {
            return idal.ModifyBy(model, whereLambda, proNames);
        }
        #endregion
        #region 根据条件查询对象
        public T GetObjet(Expression<Func<T, bool>> whereLambda)
        {
            return idal.GetObjet(whereLambda);
        }
        #endregion


        #region 根据条件查询
        public List<T> GetListBy(Expression<Func<T, bool>> whereLambda)
        {
            return idal.GetListBy(whereLambda);
        }
        #endregion

        #region 根据条件排序和查询
        /// <summary>
        /// 根据条件排序和查询
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderLambda">排序条件</param>
        /// <returns></returns>
        public List<T> GetListBy<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, bool isAsc = true)
        {
            return idal.GetListBy(whereLambda, orderLambda, isAsc);
        }
        #endregion

        #region 分页查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageize">页容量</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderLambda">排序条件</param>
        /// <returns></returns>
        public List<T> GetPageList<TKey>(int pageIndex, int pageize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda)
        {
            return idal.GetPageList(pageIndex, pageize, whereLambda, orderLambda);
        }
        #endregion

        #region 分页查询输出总行数
        /// <summary>
        /// 分页查询输出总行数
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageize">页容量</param>
        /// <param name="rowCount">总行数</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderLambda">排序条件</param>
        /// <returns></returns>
        public List<T> GetPageList<TKey>(int pageIndex, int pageize, ref int rowCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, bool isAsc = true)
        {
            return idal.GetPageList(pageIndex, pageize, ref rowCount, whereLambda, orderLambda, isAsc);
        }
        #endregion
    }
}
