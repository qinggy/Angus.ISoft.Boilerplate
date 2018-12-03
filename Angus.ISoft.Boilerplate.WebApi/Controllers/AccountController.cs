using Angus.ISoft.Boilerplate.WebApi.JwtAuth.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Angus.ISoft.Boilerplate.WebApi.Controllers
{
    /// <summary>
    /// 账户
    /// </summary>
    [RoutePrefix("api/v1/account")]
    public class AccountController : ApiController
    {
        /// <summary>
        /// 登录
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
    }
}
