/**********************************************************************************
*项目名称：Angus Isoft Boilerplate
*项目描述：EF CodeFirst映射基类
*CLR 版本：4.0.30319.18449
*版 本 号：
*作    者：Angus
*修 改 人：
*修改说明：
*创建时间：2017/05/04 15:33:10
*更新时间：
***********************************************************************************
                                * Copyright @ ISoft 2017. All rights reserved.
***********************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseEntity = Angus.ISoft.Boilerplate.Entity;

namespace Angus.ISoft.Boilerplate.Repository
{
    /// <summary>
    /// 用于实体类与数据库映射关系的配置基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EntityMapping<T> : EntityTypeConfiguration<T>, IEntityMapping where T : BaseEntity.Entity
    {
        /// <summary>
        /// 创建实体类
        /// </summary>
        protected EntityMapping()
        {
            this.HasKey(m => m.Id);
            this.Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        /// <summary>
        /// 将DbModelBuilder加入注册
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(DbModelBuilder builder)
        {
            builder.Configurations.Add<T>(this);
        }
    }
}
