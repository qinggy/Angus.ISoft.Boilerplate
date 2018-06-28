using Angus.ISoft.Boilerplate.Structuremapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Angus.ISoft.Boilerplate.WebApi.Infrastructure
{
    /// <summary>
    /// webapi 服务创建工厂
    /// </summary>
    public class ServiceActivator : IHttpControllerActivator
    {
        public ServiceActivator(HttpConfiguration configuration) { }

        public System.Web.Http.Controllers.IHttpController Create(System.Net.Http.HttpRequestMessage request,
            System.Web.Http.Controllers.HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            return ObjectFactory.Container.GetInstance(controllerType) as IHttpController;
        }
    }
}