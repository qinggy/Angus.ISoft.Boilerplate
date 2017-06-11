/**********************************************************************************
*项目名称：Angus Isoft Boilerplate
*项目描述：数据库上下文
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

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angus.ISoft.Boilerplate.Repository
{
    public sealed class ISoftDbContext : DbContext
    {
        private static readonly IEnumerable<IEntityMapping> _mappings;
        private static readonly CompositionContainer _compositionContainer;
        private static readonly string _connectionStr;

        static ISoftDbContext()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(IEntityMapping).Assembly));

            _compositionContainer = new CompositionContainer(catalog);
            _mappings = _compositionContainer.GetExportedValues<IEntityMapping>();

            _connectionStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ConnectionString;
        }

        public ISoftDbContext()
            : base(_connectionStr)
        {
            //播种Seed

            Database.SetInitializer<ISoftDbContext>(new DbContextInitializer());
        }

        /// <summary>
        /// 加入所有模型映射
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (_mappings != null)
            {
                foreach (var map in _mappings)
                {
                    map.Configure(modelBuilder);
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 播种器
        /// </summary>
        class DbContextInitializer : CreateDatabaseIfNotExists<ISoftDbContext>
        {
            protected override void Seed(ISoftDbContext context)
            {
                base.Seed(context);

                // TODO


            }
        }
    }
}
