/**********************************************************************************
*项目名称：Angus Isoft Boilerplate
*项目描述：数据库基础操作实现基类
*CLR 版本：4.0.30319.18449
*版 本 号：
*作    者：Angus
*修 改 人：
*修改说明：
*创建时间：2017/05/04 15:12:51
*更新时间：
***********************************************************************************
                                * Copyright @ ISoft 2017. All rights reserved.
***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseEntity = Angus.ISoft.Boilerplate.DbModel;
using Angus.ISoft.Boilerplate.Infrastructure;
using Angus.ISoft.Boilerplate.Repository;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;

namespace Angus.ISoft.Boilerplate.Service
{
    public abstract class BaseService<T> : IBaseService<T> where T : BaseEntity.Entity
    {
        private readonly Lazy<ISoftDbContext> _dbContext;

        protected ISoftDbContext DbContext
        {
            get
            {
                return _dbContext.Value;
            }
        }

        internal DbSet<T> DbSet;

        public BaseService()
        {
            _dbContext = new Lazy<ISoftDbContext>(() => new ISoftDbContext());
            DbSet = _dbContext.Value.Set<T>();
        }

        public void Dispose()
        {
            if (_dbContext.IsValueCreated)
            {
                _dbContext.Value.Dispose();
            }
        }

        public void DatabasePreload()
        {
            var objectContext = ((IObjectContextAdapter)DbContext).ObjectContext;
            var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
            mappingCollection.GenerateViews(new List<EdmSchemaError>());
        }

        public T SingleOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            var dbResult = DbSet.Where(whereCondition).FirstOrDefault();

            return dbResult;
        }

        public T SingleOrDefaultOrderBy(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition, System.Linq.Expressions.Expression<Func<T, int>> orderBy, DbEnum direction)
        {
            switch (direction)
            {
                case DbEnum.ASC:
                    return DbSet.Where(whereCondition).OrderBy(orderBy).FirstOrDefault();
                case DbEnum.DESC:
                    return DbSet.Where(whereCondition).OrderByDescending(orderBy).FirstOrDefault();
                default:
                    return DbSet.Where(whereCondition).OrderBy(orderBy).FirstOrDefault();
            }
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.AsEnumerable();
        }

        public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            return DbSet.Where(whereCondition).AsEnumerable();
        }

        public IEnumerable<T> GetPagedRecords(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition, System.Linq.Expressions.Expression<Func<T, object>> orderBy, int pageNo, int pageSize)
        {
            return (DbSet.Where(whereCondition).OrderBy(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize)).AsEnumerable();
        }

        public bool Exists(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            return DbSet.Any(whereCondition);
        }

        public int Count(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            return DbSet.Where(whereCondition).Count();
        }

        public T Insert(T entity)
        {
            dynamic obj = DbSet.Attach(entity);
            this.DbContext.Entry(entity).State = EntityState.Added;
            this.DbContext.SaveChanges();
            return obj;
        }

        public Guid AddEntity(T entity)
        {
            DbSet.Add(entity);
            this.DbContext.SaveChanges();
            return entity.Id;
        }

        public void Update(T entity)
        {
            //this.DbSet.Attach(entity);
            //this.DbContext.Entry(entity).State = EntityState.Modified;
            var ff = DbSet.FirstOrDefault(a => a.Id == entity.Id);
            if (ff != null)
            {
                ff.CopyFrom(entity);
            }
            this.DbContext.SaveChanges();
        }

        public void UpdateAll(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                this.DbSet.Attach(entity);
                this.DbContext.Entry(entity).State = EntityState.Modified;
            }
            this.DbContext.SaveChanges();
        }

        public void Delete(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            IEnumerable<T> entities = this.GetAll(whereCondition);
            foreach (T entity in entities)
            {
                if (DbContext.Entry(entity).State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }
                DbSet.Remove(entity);
            }
            this.DbContext.SaveChanges();
        }

        public IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return DbSet.SqlQuery(query, parameters);
        }

        public IEnumerable<TEntity> ExecWithStoreProcedure<TEntity>(string query, params object[] parameters) where TEntity : class
        {
            return DbContext.Database.SqlQuery<TEntity>(query, parameters);
        }

        public int ExecuteSqlCommand(string sql, params System.Data.SqlClient.SqlParameter[] paras)
        {
            return DbContext.Database.ExecuteSqlCommand(sql, paras);
        }
    }
}
