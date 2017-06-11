/**********************************************************************************
*项目名称：Angus Isoft Boilerplate
*项目描述：
*CLR 版本：4.0.30319.42000
*版 本 号：
*作    者：Angus
*修 改 人：
*修改说明：
*创建时间：2016/11/8 13:53:23
*更新时间：
***********************************************************************************
                                * Copyright (c) ISoft 2017. All rights reserved.
***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Angus.ISoft.Boilerplate.Infrastructure
{
    [DataContract]
    public class ResponseStatus
    {
        [DataMember]
        public bool IsSuccess { get; set; }
        [DataMember]
        public object Content { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string RedirectUrl { get; set; }
        [DataMember]
        public string Remark { get; set; }
    }
}