/**********************************************************************************
*项目名称：ISoft
*项目描述：映射接口
*CLR 版本：4.0.30319.18449
*版 本  号：
*作      者：卿光扬
*修 改  人：
*修改说明：
*创建时间：2017/05/04 15:20:10
*更新时间：
***********************************************************************************
                                * Copyright @ ISoft 2017. All rights reserved.
***********************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Angus.ISoft.Boilerplate.Repository
{
    /// <summary>
    /// 用于动态注册EntityTypeConfiguration
    /// </summary>
    [InheritedExport(typeof(IEntityMapping))]
    public interface IEntityMapping
    {
        /// <summary>
        /// 添加EntityTypeConfiguration
        /// </summary>
        /// <param name="builder"></param>
        void Configure(DbModelBuilder builder);
    }
}
