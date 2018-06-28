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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angus.ISoft.Boilerplate.DbModel
{
    public abstract class Entity
    {
        /// <summary>
        /// 实体类主键（采用Guid转换成String）
        /// </summary>
        public Guid Id { get; set; }
    }
}
