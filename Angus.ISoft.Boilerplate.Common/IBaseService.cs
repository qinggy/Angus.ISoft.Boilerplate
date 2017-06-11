/**********************************************************************************
*项目名称：Angus Isoft Boilerplate
*项目描述：
*CLR 版本：4.0.30319.18449
*版 本 号：
*作    者：Angus
*修 改 人：
*修改说明：
*创建时间：
*更新时间：
***********************************************************************************
                                * Copyright @ ISoft 2017. All rights reserved.
***********************************************************************************/

using Angus.ISoft.Boilerplate.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Angus.ISoft.Boilerplate.Service
{
    public interface IBaseService<T> : IDisposable
    {
        /// <summary>
        /// 通过给定条件查询单一值，如果没有返回null
        /// </summary>
        /// <param name="primaryKey">Prmary key to find</param>
        /// <returns>T</returns>
        T SingleOrDefault(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        ///  通过给定条件排序字段以及排序方式查询单一值，如果没有返回null
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <param name="orderBy"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        T SingleOrDefaultOrderBy(Expression<Func<T, bool>> whereCondition, Expression<Func<T, int>> orderBy, DbEnum direction = DbEnum.ASC);

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// 通过给定条件查询所有
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<T> GetPagedRecords(Expression<Func<T, bool>> whereCondition, Expression<Func<T, object>> orderBy, int pageNo, int pageSize);

        /// <summary>
        /// 通过给定条件判断是否存在
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        bool Exists(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// 查询给定条件的对象条数
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        /// <param name="userId">The user performing the insert</param>
        /// <returns></returns>
        T Insert(T entity);

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Guid AddEntity(T entity);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="userId">The user performing the update</param>
        void Update(T entity);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entities">Entities to update</param>
        void UpdateAll(IList<T> entities);

        /// <summary>
        /// 删除给定条件的所有数据
        /// ** 提示 - 所有项需要被EF上下文管理
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="userId">The user Id who deleted the entity</param>
        /// <returns></returns>
        void Delete(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// 查询存储过程
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        int ExecuteSqlCommand(string sql, params SqlParameter[] paras);
    }
}
