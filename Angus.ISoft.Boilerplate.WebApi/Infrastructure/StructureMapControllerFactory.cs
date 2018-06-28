/**********************************************************************************
*项目名称：Angus Isoft Boilerplate
*项目描述：继承MVC DefaultControllerFactory 让默认构造Controller实现自定义功能
*CLR 版本：4.0.30319.42000
*版 本 号：
*作    者：Angus
*修 改 人：
*修改说明：
*创建时间：2016/11/7 14:04:11
*更新时间：
***********************************************************************************
                                * Copyright (c) ISoft 2017. All rights reserved.
***********************************************************************************/

using Angus.ISoft.Boilerplate.Structuremapper;
using StructureMap;
using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Angus.ISoft.Boilerplate.Infrastructure
{
    /// <summary>
    /// MVC Controller初始化工厂
    /// </summary>
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (requestContext == null || controllerType == null) return null;

            return (Controller)ObjectFactory.Container.GetInstance(controllerType);
        }
    }

    public static class BootStrapStructureMapper
    {
        public static void Register()
        {
            // MVC替换Controller默认创建工厂
            // ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
            ObjectFactory.Initialize<PublicRepositoryRegistry>();
        }
    }
}