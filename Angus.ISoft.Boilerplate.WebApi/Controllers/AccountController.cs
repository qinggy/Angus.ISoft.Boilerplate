using Angus.ISoft.Boilerplate.Domain;
using Angus.ISoft.Boilerplate.Infrastructure;
using Angus.ISoft.Boilerplate.Service;
using Angus.ISoft.Boilerplate.WebApi.ExtensionAttribute;
using Angus.ISoft.Boilerplate.WebApi.JwtAuth.Simple;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Angus.ISoft.Boilerplate.WebApi.Controllers
{
    /// <summary>
    /// 账户模块
    /// </summary>
    [RoutePrefix("api/v1/account")]
    public class AccountController : ApiController
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService _accountService)
        {
            accountService = _accountService;
        }

        protected override void Dispose(bool disposing)
        {
            accountService.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost, Route("login/{username}/{password}")]
        public string Login(string username, string password)
        {
            var userId = "1";
            return JwtManager.GenerateToken(userId, username);
        }

        /// <summary>
        /// 获取当前账号信息
        /// </summary>
        /// <param name="accountId">账号编码</param>
        /// <returns></returns>
        /// [JwtAuth]
        [HttpGet, Route("getaccountinfo/{accountId}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(AccountDto))]
        public HttpResponseMessage GetAccountInfo(Guid accountId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseStatus
            {
                IsSuccess = true,
                Code = "00",
                Content = accountService.GetAccountInfo(accountId).Result
            });
        }
    }
}
