using Angus.ISoft.Boilerplate.WebApi.JwtAuth.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace Angus.ISoft.Boilerplate.WebApi.ExtensionAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class JwtAuthAttribute : AuthorizeAttribute
    {
        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return base.OnAuthorizationAsync(actionContext, cancellationToken);
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var request = actionContext.Request;
            var authorization = request.Headers.Authorization;

            if (authorization == null || authorization.Scheme != "Bearer") return false;

            if (string.IsNullOrEmpty(authorization.Parameter)) return false;

            var token = authorization.Parameter;
            var userId = "1"; // from session
            return JwtManager.ValidateToken(token, userId);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
        }
    }
}