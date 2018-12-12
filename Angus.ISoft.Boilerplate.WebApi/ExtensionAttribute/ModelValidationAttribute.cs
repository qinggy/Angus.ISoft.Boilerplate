/**********************************************************************************
*项目名称：Angus Isoft Boilerplate
*项目描述：
*CLR 版本：4.0.30319.42000
*版 本 号：
*作    者：Angus
*修 改 人：
*修改说明：
*创建时间：2018/3/27 0:07:57
*更新时间：
***********************************************************************************/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Http.Filters;

namespace Angus.ISoft.Boilerplate.WebApi.ExtensionAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;
            if (!modelState.IsValid)
            {
                string error = string.Empty;
                foreach (var key in modelState.Keys)
                {
                    var state = modelState[key];
                    if (state.Errors.Any())
                    {
                        error = state.Errors.First().ErrorMessage;
                        break;
                    }
                }
                var response = new { Status = HttpStatusCode.InternalServerError, Message = error };
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Accepted)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(response), System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
                };
            }
        }
    }
}