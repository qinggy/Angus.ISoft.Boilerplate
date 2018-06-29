using Angus.ISoft.Boilerplate.Infrastructure.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Angus.ISoft.Boilerplate.WebApi.ExtensionAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ActionLogAttribute : BaseLogAttribute, IExceptionFilter
    {
        public ActionLogAttribute(LogTypeEnum logType)
            : base(logType)
        {

        }

        public void OnException(ExceptionContext filterContext)
        {
            base.WriteLog();
        }
    }
}