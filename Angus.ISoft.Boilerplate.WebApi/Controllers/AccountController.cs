using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Angus.ISoft.Boilerplate.WebApi.Controllers
{
    [RoutePrefix("api/v1/account")]
    public class AccountController : ApiController
    {
        [HttpPost, Route("login")]
        public IHttpActionResult Login()
        {
            return Ok(new { id = 1, name = 2 });
        }
    }
}
