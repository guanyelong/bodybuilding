using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BBD.IBLL
{
    public interface IBaseBLL<T>
    {
        #region 数据库名称
        string DbName { set; }
        #endregion

        #region 新增
        int Add(T model);
        #endregion

        #region 根据Id删除
        int Del(T model);
        #endregion

        #region 根据条件删除
        int DelBy(Expression<Func<T, bool>> delWhere);
        #endregion

        #region 修改
        int Modify(T model, params string[] proNames);

        int Modifyed(T model, params string[] proNames);
        int Modify(T model, params  Expression<Func<T, object>>[] ignorePerperties);
        #endregion

        #region 批量修改
        int ModifyBy(T model, Expression<Func<T, bool>> whereLambda, params string[] proNames);
        #endregion

        #region 根据条件查询对象
        T GetObjet(Expression<Func<T, bool>> whereLambda);
        #endregion


        #region 根据条件查询
        List<T> GetListBy(Expression<Func<T, bool>> whereLambda);
        #endregion

        #region 根据条件排序和查询
        /// <summary>
        /// 根据条件排序和查询
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderLambda">排序条件</param>
        /// <returns></returns>
        List<T> GetListBy<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, bool isAsc = true);
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
        List<T> GetPageList<TKey>(int pageIndex, int pageize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda);
        #endregion

        #region 分页查询输出总行数
        /// <summary>
        /// 根据条件排序和查询
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageize">页容量</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderLambda">排序条件</param>
        /// <returns></returns>
        List<T> GetPageList<TKey>(int pageIndex, int pageize, ref int rowCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, bool isAsc = true);
        #endregion
    }
}
