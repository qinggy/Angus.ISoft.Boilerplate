/**********************************************************************************
*项目名称：Angus Isoft Boilerplate
*项目描述：接口和具体实现对应类Ioc。
*CLR 版本：4.0.30319.42000
*版 本 号：
*作    者：Angus
*修 改 人：
*修改说明：
*创建时间：2016/11/8 9:27:42
*更新时间：
***********************************************************************************
                                * Copyright (c) ISoft 2017. All rights reserved.
***********************************************************************************/


using Angus.ISoft.Boilerplate.DbModel;
using Angus.ISoft.Boilerplate.Service;
using StructureMap;

namespace Angus.ISoft.Boilerplate.WebApi.Core
{
    public class PublicRepositoryRegistry : Registry
    {
        //映射关系关联
        public PublicRepositoryRegistry()
        {
            //For<IDemoService>().Use<DemoService>();
            Scan(s =>
            {
                s.AssembliesFromApplicationBaseDirectory(f =>
                {
                    return f.FullName.Contains("Angus.ISoft.Boilerplate.Service");
                });
                s.WithDefaultConventions();
            });
        }
    }
}